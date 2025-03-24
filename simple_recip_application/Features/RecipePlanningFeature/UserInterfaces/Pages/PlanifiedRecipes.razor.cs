using System.Globalization;
using System.Linq.Expressions;
using Fluxor;
using Microsoft.AspNetCore.Components;
using simple_recip_application.Constants;
using simple_recip_application.Extensions;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Entities;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Factories;
using simple_recip_application.Features.RecipePlanningFeature.Store;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.RecipePlanningFeature.UserInterfaces.Pages;

public partial class PlanifiedRecipes
{
    [Inject] public required IState<PlanifiedRecipeState> PlanifiedRecipeState { get; set; }
    [Inject] public required IDispatcher Dispatcher { get; set; }
    [Inject] public required IPlanifiedRecipeModelFactory PlanifiedRecipeFactory { get; set; }
    [Inject] public required NavigationManager NavigationManager { get; set; }

    private Dictionary<DayOfWeek, List<IPlanifiedRecipeModel>> _recipesByDay = new();
    private bool _isRecipeSelectionModalVisible = false;
    private DayOfWeek _selectedDayForPlanning;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        var startOfWeek = DateTime.UtcNow.StartOfWeek(DayOfWeek.Monday);
        var endOfWeek = DateTime.UtcNow.EndOfWeek(DayOfWeek.Monday);

        Expression<Func<IPlanifiedRecipeModel, bool>> predicate =
            c => c.PlanifiedDateTime >= startOfWeek && c.PlanifiedDateTime <= endOfWeek;

        Dispatcher.Dispatch(new LoadItemsAction<IPlanifiedRecipeModel>(Predicate: predicate));
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        GroupRecipesByDay();
    }

    private void GroupRecipesByDay()
    {
        _recipesByDay = Enum.GetValues<DayOfWeek>()
            .OrderBy(d => d == DayOfWeek.Sunday ? 7 : (int)d)
            .ToDictionary(day => day, day => new List<IPlanifiedRecipeModel>());

        foreach (var recipe in PlanifiedRecipeState.Value.Items)
        {
            _recipesByDay[recipe.PlanifiedDateTime.DayOfWeek].Add(recipe);
        }
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

    private static IEnumerable<(DayOfWeek DayOfWeek, string FormattedDate)> GetOrderedDaysOfWeek()
    {
        var startOfWeek = DateTime.UtcNow.StartOfWeek(DayOfWeek.Monday);

        var daysOfWeekWithFullDates = Enumerable.Range(0, 7)
            .Select(i => startOfWeek.AddDays(i))
            .Select(date => (date.DayOfWeek, date.ToString("dddd dd MMMM yyyy", CultureInfo.CurrentCulture).ToUpper()))
            .OrderBy(tuple => tuple.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)tuple.DayOfWeek)
            .ToList();

        return daysOfWeekWithFullDates;
    }
}
