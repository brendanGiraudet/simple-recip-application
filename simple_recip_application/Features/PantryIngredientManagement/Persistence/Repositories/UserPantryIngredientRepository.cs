using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using simple_recip_application.Data;
using simple_recip_application.Data.Persistence.Repositories;
using simple_recip_application.Dtos;
using simple_recip_application.Extensions;
using simple_recip_application.Features.IngredientsManagement.UserInterfaces.Pages.Ingredients;
using simple_recip_application.Features.PantryIngredientManagement.ApplicationCore.Entities;
using simple_recip_application.Features.PantryIngredientManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.PantryIngredientManagement.Persistence.Entities;

namespace simple_recip_application.Features.PantryIngredientManagement.Persistence.Repositories;

public class UserPantryIngredientRepository(ApplicationDbContext dbContext)
    : Repository<UserPantryIngredientModel>(dbContext), IUserPantryIngredientRepository
{
    public async Task<MethodResult<IEnumerable<IUserPantryIngredientModel>>> GetUserPantryIngredientsAsync(string userId)
    {
        try
        {
            var ingredients = await dbContext.Set<UserPantryIngredientModel>()
            .Include(x => x.Ingredient)
            .Where(x => x.UserId == userId)
            .ToListAsync();

            return new MethodResult<IEnumerable<IUserPantryIngredientModel>>(true, ingredients);
        }
        catch (Exception ex)
        {
            return new MethodResult<IEnumerable<IUserPantryIngredientModel>>(false, []);
        }
    }

    public async Task<MethodResult<IUserPantryIngredientModel?>> GetUserPantryIngredientAsync(string userId, Guid ingredientId)
    {
        try
        {
            var ingredient = await dbContext.Set<UserPantryIngredientModel>()
            .Include(x => x.Ingredient)
            .FirstOrDefaultAsync(x => x.UserId == userId && x.IngredientId == ingredientId);

            return new MethodResult<IUserPantryIngredientModel?>(true, ingredient);
        }
        catch (Exception ex)
        {
            return new MethodResult<IUserPantryIngredientModel?>(false, null);
        }
    }

    public async Task<MethodResult> AddAsync(IUserPantryIngredientModel entity)
        => await base.AddAsync(entity as UserPantryIngredientModel);

    public async Task<MethodResult> UpdateAsync(IUserPantryIngredientModel entity)
        => await base.UpdateAsync(entity as UserPantryIngredientModel);

    public async Task<MethodResult> DeleteAsync(IUserPantryIngredientModel entity)
        => await base.DeleteAsync(entity as UserPantryIngredientModel);

    public async Task<MethodResult<IUserPantryIngredientModel?>> GetByIdAsync(Guid? id)
    {
        var result = await base.GetByIdAsync(id);

        return new MethodResult<IUserPantryIngredientModel?>(result.Success, result.Item);
    }

    public async Task<MethodResult<IEnumerable<IUserPantryIngredientModel>>> GetAsync()
    {
        var result = await base.GetAsync();

        return new MethodResult<IEnumerable<IUserPantryIngredientModel>>(result.Success, result.Item);
    }

    public async Task<MethodResult<IEnumerable<IUserPantryIngredientModel>>> GetAsync(int take, int skip, Expression<Func<IUserPantryIngredientModel, bool>>? predicate = null)
    {
        var convertedPredicate = predicate?.Convert<IUserPantryIngredientModel, UserPantryIngredientModel, bool>();

        var result = await base.GetAsync(take, skip, convertedPredicate);

        return new MethodResult<IEnumerable<IUserPantryIngredientModel>>(result.Success, result.Item);
    }

    public async Task<MethodResult> AddRangeAsync(IEnumerable<IUserPantryIngredientModel>? entities)
    {
        return await base.AddRangeAsync(entities?.Cast<UserPantryIngredientModel>() ?? []);
    }
}
