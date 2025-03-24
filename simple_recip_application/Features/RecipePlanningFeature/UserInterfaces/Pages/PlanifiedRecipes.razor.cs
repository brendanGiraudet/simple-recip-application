using System.Globalization;
using System.Linq.Expressions;
using Fluxor;
using Microsoft.AspNetCore.Components;
using simple_recip_application.Constants;
using simple_recip_application.Extensions;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Entities;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Factories;
using simple_recip_application.Features.RecipePlanningFeature.Store;
using simple_recip_application.Features.RecipePlanningFeature.Store.Actions;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.RecipePlanningFeature.UserInterfaces.Pages;

public partial class PlanifiedRecipes
{
    [Inject] public required IState<PlanifiedRecipeState> PlanifiedRecipeState { get; set; }
    [Inject] public required IDispatcher Dispatcher { get; set; }
    [Inject] public required IPlanifiedRecipeModelFactory PlanifiedRecipeFactory { get; set; }
    [Inject] public required NavigationManager NavigationManager { get; set; }

    private bool _isRecipeSelectionModalVisible = false;
    private DayOfWeek _selectedDayForPlanning;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        LoadRecipesForCurrentWeek();
    }

    private void LoadRecipesForCurrentWeek()
    {
        var startOfWeek = PlanifiedRecipeState.Value.CurrentWeekStart.Date.StartOfDay();
        var endOfWeek = PlanifiedRecipeState.Value.CurrentWeekStart.EndOfWeek().EndOfDay();

        Expression<Func<IPlanifiedRecipeModel, bool>> predicate =
            c => c.PlanifiedDateTime >= startOfWeek && c.PlanifiedDateTime <= endOfWeek;

        Dispatcher.Dispatch(new LoadItemsAction<IPlanifiedRecipeModel>(Predicate: predicate));
    }


    private void LoadPreviousWeek()
    {
        Dispatcher.Dispatch(new SetCurrentWeekStartAction(PlanifiedRecipeState.Value.CurrentWeekStart.AddDays(-7)));

        LoadRecipesForCurrentWeek();
    }

    private void LoadNextWeek()
    {
        Dispatcher.Dispatch(new SetCurrentWeekStartAction(PlanifiedRecipeState.Value.CurrentWeekStart.AddDays(7)));

        LoadRecipesForCurrentWeek();
    }

    private void OpenRecipeDetails(Guid recipeId)
    {
        NavigationManager.NavigateTo(PageUrlsConstants.GetRecipeDetailsPage(recipeId));
    }

    private void OpenRecipeSelectionModal(DayOfWeek day)
    {
        _selectedDayForPlanning = day;
        _isRecipeSelectionModalVisible = true;
    }

    private void CloseRecipeSelectionModal(bool _)
    {
        _isRecipeSelectionModalVisible = false;
    }

    private void HandleRecipeSelected(IRecipeModel selectedRecipe)
    {
        var planifiedDateTime = DateTime.UtcNow.StartOfWeek(DayOfWeek.Monday)
            .AddDays((int)_selectedDayForPlanning - 1)
            .Date;

        var planifiedRecipe = PlanifiedRecipeFactory.CreatePlanifiedRecipeModel(
            recipe: selectedRecipe,
            planifiedDatetime: planifiedDateTime,
            userId: "currentUserId" // Remplacer par UserId réel selon le contexte
        );

        Dispatcher.Dispatch(new AddItemAction<IPlanifiedRecipeModel>(planifiedRecipe));

        _isRecipeSelectionModalVisible = false;
    }

    private IEnumerable<(DayOfWeek DayOfWeek, string FormattedDate)> GetOrderedDaysOfWeek()
    {
        var startOfWeek = PlanifiedRecipeState.Value.CurrentWeekStart;

        var daysOfWeekWithFullDates = Enumerable.Range(0, 7)
            .Select(i => startOfWeek.AddDays(i))
            .Select(date => (date.DayOfWeek, date.ToString("dddd dd MMMM yyyy", CultureInfo.CurrentCulture).ToUpper()))
            .OrderBy(tuple => tuple.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)tuple.DayOfWeek)
            .ToList();

        return daysOfWeekWithFullDates;
    }
}