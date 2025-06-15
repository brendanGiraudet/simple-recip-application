using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using simple_recip_application.Data;
using simple_recip_application.Data.Persistence.Repositories;
using simple_recip_application.Dtos;
using simple_recip_application.Extensions;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.RecipesManagement.Persistence.Entites;

namespace simple_recip_application.Features.RecipesManagement.Persistence.Repositories;

public class RecipeRepository
(
    ApplicationDbContext _dbContext
)
: EntityBaseRepository<RecipeModel>(_dbContext), IRecipeRepository
{
    public new async Task<MethodResult<IRecipeModel?>> GetByIdAsync(Guid? id)
    {
        try
        {
            var recipe = await _dbContext.Set<RecipeModel>()
                                         .Where(c => c.Id == id)
                                         .Include(c => c.Ingredients)
                                            .ThenInclude(c => c.Ingredient)
                                         .Include(c => c.Tags)
                                            .ThenInclude(c => c.Tag)
                                         .AsSplitQuery()
                                         .FirstOrDefaultAsync();

            return new MethodResult<IRecipeModel?>(true, recipe);
        }
        catch (System.Exception ex)
        {
            return new MethodResult<IRecipeModel?>(false, null);
        }
    }

    public new async Task<MethodResult<IEnumerable<IRecipeModel>>> GetAsync()
    {
        var result = await base.GetAsync();

        return new MethodResult<IEnumerable<IRecipeModel>>(result.Success, result.Item);
    }

    public async Task<MethodResult<IEnumerable<IRecipeModel>>> GetAsync(int take, int skip, Expression<Func<IRecipeModel, bool>>? predicate = null, Expression<Func<IRecipeModel, object>>? sort = null)
    {
        try
        {
            if (sort is null)
                sort = c => c.Name;

            var convertedPredicate = predicate?.Convert<IRecipeModel, RecipeModel, bool>();
            var convertedSort = sort?.Convert<IRecipeModel, RecipeModel, object>();

            var recipesRequest = base.Get(take, skip, convertedPredicate, convertedSort)
                                     .Select(c => new RecipeModel
                                     {
                                         Id = c.Id,
                                         Name = c.Name,
                                         Image = c.Image,
                                         CookingTime = c.CookingTime,
                                         PreparationTime = c.PreparationTime,
                                     });

            var recipes = await recipesRequest.Cast<IRecipeModel>().ToListAsync();

            return new MethodResult<IEnumerable<IRecipeModel>>(true, recipes);
        }
        catch (System.Exception ex)
        {
            return new MethodResult<IEnumerable<IRecipeModel>>(false, []);
        }
    }

    public async Task<MethodResult> AddAsync(IRecipeModel? entity)
    {
        var recipeModel = entity as RecipeModel;

        foreach (var ingredient in recipeModel.Ingredients)
        {
            _dbContext.Attach(ingredient.Ingredient);
        }

        foreach (var tag in recipeModel.Tags ?? [])
        {
            _dbContext.Attach(tag.Tag);
        }

        return await base.AddAsync(entity as RecipeModel);
    }

    public async Task<MethodResult> AddRangeAsync(IEnumerable<IRecipeModel>? entities)
    {
        return await base.AddRangeAsync(entities?.Cast<RecipeModel>() ?? []);
    }

    public async Task<MethodResult> UpdateAsync(IRecipeModel? entity)
    {
        if (entity?.Id is null)
            return new MethodResult(false);

        // Récupération complète de la recette actuelle (avec ingrédients associés)
        var actualRecipeResult = await GetByIdAsync(entity.Id);
        if (!actualRecipeResult.Success || actualRecipeResult.Item is null)
            return new MethodResult(false);

        var existingRecipe = actualRecipeResult.Item;

        UpdateIngredients(entity, existingRecipe);

        UpdateTags(entity, existingRecipe);

        // Mettre à jour les autres propriétés de la recette existante
        existingRecipe.Name = entity.Name;
        existingRecipe.Description = entity.Description;
        existingRecipe.Instructions = entity.Instructions;
        existingRecipe.CookingTime = entity.CookingTime;
        existingRecipe.PreparationTime = entity.PreparationTime;
        existingRecipe.Image = entity.Image;
        existingRecipe.Category = entity.Category;

        // Sauvegarde finale des modifications
        await _dbContext.SaveChangesAsync();

        return new MethodResult(true);
    }

    private void UpdateIngredients(IRecipeModel entity, IRecipeModel existingRecipe)
    {
        // Simplification des accès en dictionnaires pour performance (O(1))
        var existingIngredientsDict = existingRecipe.IngredientModels
            .ToDictionary(i => i.IngredientModel.Id, i => i);

        var updatedIngredientsDict = entity.IngredientModels
            .ToDictionary(i => i.IngredientModel.Id, i => i);

        // Détection des ingrédients à supprimer
        var ingredientsToRemove = existingIngredientsDict.Keys
            .Except(updatedIngredientsDict.Keys)
            .Select(id => existingIngredientsDict[id])
            .ToList();

        // Suppression des ingrédients supprimés
        foreach (var ingredient in ingredientsToRemove)
        {
            _dbContext.Remove(ingredient);
        }

        // Mise à jour des ingrédients existants
        foreach (var (ingredientId, existingIngredient) in existingIngredientsDict)
        {
            if (updatedIngredientsDict.TryGetValue(ingredientId, out var updatedIngredient))
            {
                // Vérifier si la quantité a changé
                if (existingIngredient.Quantity != updatedIngredient.Quantity)
                {
                    existingIngredient.Quantity = updatedIngredient.Quantity;
                    _dbContext.Entry(existingIngredient).State = EntityState.Modified;
                }
                else
                {
                    // Aucune modification nécessaire
                    _dbContext.Entry(existingIngredient).State = EntityState.Unchanged;
                }
            }
        }

        // Ajout des nouveaux ingrédients (présents dans updated mais absents dans existing)
        var ingredientsToAdd = updatedIngredientsDict.Keys
            .Except(existingIngredientsDict.Keys)
            .Select(id => updatedIngredientsDict[id])
            .ToList();

        foreach (var newIngredient in ingredientsToAdd)
        {
            existingRecipe.IngredientModels.Add(newIngredient);
            _dbContext.Entry(newIngredient).State = EntityState.Added;
        }
    }

    private void UpdateTags(IRecipeModel entity, IRecipeModel existingRecipe)
    {
        // Simplification des accès en dictionnaires pour performance (O(1))
        var existingTagsDict = existingRecipe.TagModels
            .ToDictionary(i => i.TagModel.Id, i => i);

        var updatedTagsDict = entity.TagModels
            .ToDictionary(i => i.TagModel.Id, i => i);

        // Détection des tags à supprimer
        var tagsToRemove = existingTagsDict.Keys
            .Except(updatedTagsDict.Keys)
            .Select(id => existingTagsDict[id])
            .ToList();

        // Suppression des tags supprimés
        foreach (var ingredient in tagsToRemove)
        {
            _dbContext.Remove(ingredient);
        }

        // Ajout des nouveaux tags (présents dans updated mais absents dans existing)
        var tagsToAdd = updatedTagsDict.Keys
            .Except(existingTagsDict.Keys)
            .Select(id => updatedTagsDict[id])
            .ToList();

        foreach (var newTag in tagsToAdd)
        {
            existingRecipe.TagModels.Add(newTag);
            _dbContext.Entry(newTag).State = EntityState.Added;
        }
    }

    public async Task<MethodResult> DeleteAsync(IRecipeModel? entity)
    {
        return await base.DeleteAsync(entity as RecipeModel);
    }

    public async Task<MethodResult<int>> CountAsync(Expression<Func<IRecipeModel, bool>>? predicate = null)
    {
        var convertedPredicate = predicate?.Convert<IRecipeModel, RecipeModel, bool>();

        return await base.CountAsync(convertedPredicate);
    }

}
