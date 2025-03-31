using Fluxor;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Entities;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.EqualityComparers;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Repositories;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Services;
using simple_recip_application.Features.RecipePlanningFeature.Store.Actions;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.RecipesManagement.Store;

public class PlanifiedRecipeEffects
(
    IPlanifiedRecipeRepository _planifiedRecipeRepository,
    IRecipePlanifierService _recipePlanifierService
)
{
    [EffectMethod]
    public async Task HandleLoadItemsAction(LoadItemsAction<IPlanifiedRecipeModel> action, IDispatcher dispatcher)
    {
        var result = await _planifiedRecipeRepository.GetAsync(action.Take, action.Skip, action.Predicate);

        if (result.Success)
            dispatcher.Dispatch(new LoadItemsSuccessAction<IPlanifiedRecipeModel>(result.Item));

        else
            dispatcher.Dispatch(new LoadItemsFailureAction<IPlanifiedRecipeModel>());
    }

    [EffectMethod]
    public async Task HandleAddItemAction(AddItemAction<IPlanifiedRecipeModel> action, IDispatcher dispatcher)
    {
        var result = await _planifiedRecipeRepository.AddAsync(action.Item);

        if (result.Success)
            dispatcher.Dispatch(new AddItemSuccessAction<IPlanifiedRecipeModel>(action.Item));

        else
            dispatcher.Dispatch(new AddItemFailureAction<IPlanifiedRecipeModel>(action.Item));
    }

    [EffectMethod]
    public async Task HandleDeleteItemAction(DeleteItemAction<IPlanifiedRecipeModel> action, IDispatcher dispatcher)
    {
        var result = await _planifiedRecipeRepository.DeleteAsync(action.Item);

        if (result.Success)
            dispatcher.Dispatch(new DeleteItemSuccessAction<IPlanifiedRecipeModel>(action.Item));

        else
            dispatcher.Dispatch(new DeleteItemFailureAction<IPlanifiedRecipeModel>(action.Item));
    }

    [EffectMethod]
    public async Task HandlePlanifiedRecipesForTheWeekAction(PlanifiedRecipesForTheWeekAction action, IDispatcher dispatcher)
    {
        var result = await _recipePlanifierService.GetPlanifiedRecipesForTheWeekAsync(action.CurrentDate);

        if (!result.Success)
        {
            dispatcher.Dispatch(new PlanifiedRecipesForTheWeekFailureAction());

            return;
        }

        var recipesList = result.Item.SelectMany(entry => entry.Value).ToList();

        var addResult = await _planifiedRecipeRepository.AddRangeAsync(recipesList);

        if (addResult.Success)
            dispatcher.Dispatch(new PlanifiedRecipesForTheWeekSuccessAction(result.Item));

        else
            dispatcher.Dispatch(new PlanifiedRecipesForTheWeekFailureAction());
    }

    [EffectMethod]
    public async Task HandlePlanifiedRecipeAutomaticalyAction(PlanifiedRecipeAutomaticalyAction action, IDispatcher dispatcher)
    {
        var result = await _recipePlanifierService.GetPlanifiedRecipeAutomaticalyAsync(action.PlanifiedRecipe);

        if (!result.Success)
            dispatcher.Dispatch(new PlanifiedRecipeAutomaticalyFailureAction());

        else
            dispatcher.Dispatch(new PlanifiedRecipeAutomaticalySuccessAction(action.PlanifiedRecipe, result.Item));
    }

    [EffectMethod]
    public async Task HandlePlanifiedRecipeAutomaticalySuccessAction(PlanifiedRecipeAutomaticalySuccessAction action, IDispatcher dispatcher)
    {
        if (new PlanifiedRecipeEqualityComparer().Equals(action.OldPlanifiedRecipe, action.NewPlanifiedRecipe))
            return;

        dispatcher.Dispatch(new AddItemAction<IPlanifiedRecipeModel>(action.NewPlanifiedRecipe));

        if (action.OldPlanifiedRecipe.RecipeModel is not null)
            dispatcher.Dispatch(new DeleteItemAction<IPlanifiedRecipeModel>(action.OldPlanifiedRecipe));

        await Task.CompletedTask;
    }
}
