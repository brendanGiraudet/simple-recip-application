using System.Globalization;
using System.Linq.Expressions;
using System.Text;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using simple_recip_application.Components.OptionsMenu;
using simple_recip_application.Constants;
using simple_recip_application.Extensions;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Factories;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Entities;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Factories;
using simple_recip_application.Features.RecipePlanningFeature.Store;
using simple_recip_application.Features.RecipePlanningFeature.Store.Actions;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using simple_recip_application.Features.RecipePlanningFeature.Enums;
using simple_recip_application.Resources;
using simple_recip_application.Store.Actions;
using simple_recip_application.Enums;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.EqualityComparers;
using Microsoft.FeatureManagement;
using simple_recip_application.Features.UserInfos.Store;
using simple_recip_application.Features.ShoppingListManagement.ApplicationCore.Services;
using simple_recip_application.Features.ShoppingListManagement.Store.Actions;

namespace simple_recip_application.Features.RecipePlanningFeature.UserInterfaces.Pages.PlanifiedRecipes;


public partial class PlanifiedRecipes
{
    [Inject] public required IFeatureManager FeatureManager { get; set; }
    [Inject] public required IState<PlanifiedRecipeState> PlanifiedRecipeState { get; set; }
    [Inject] public required IDispatcher Dispatcher { get; set; }
    [Inject] public required IPlanifiedRecipeModelFactory PlanifiedRecipeFactory { get; set; }
    [Inject] public required NavigationManager NavigationManager { get; set; }
    [Inject] public required IState<UserInfosState> UserInfosState { get; set; }

    private bool _isRecipeModalVisible = false;
    private ModalModeEnum _recipeModalMode;

    private DayOfWeek _selectedDayForPlanning;
    private IPlanifiedRecipeModel? _selectedPlanifiedRecipe;
    private MomentOfTheDayEnum _selectedMomentOfTheDay = MomentOfTheDayEnum.Evening;

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
            c => c.PlanifiedDateTime >= startOfWeek && c.PlanifiedDateTime <= endOfWeek && c.UserId == UserInfosState.Value.UserInfo.Id;

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
        _recipeModalMode = ModalModeEnum.Add;
        _selectedDayForPlanning = day;
        _isRecipeModalVisible = true;
        _selectedMomentOfTheDay = MomentOfTheDayEnum.Evening;
    }

    private void OpenChangeRecipeModal(IPlanifiedRecipeModel recipeToChange)
    {
        _recipeModalMode = ModalModeEnum.Edit;
        _selectedPlanifiedRecipe = recipeToChange;
        Enum.TryParse(recipeToChange.MomentOftheDay, out _selectedMomentOfTheDay);
        _isRecipeModalVisible = true;
    }

    private void CloseRecipeModal(bool _)
    {
        _isRecipeModalVisible = false;
        _selectedPlanifiedRecipe = null;
    }

    private void HandleRecipeSelected(IRecipeModel selectedRecipe)
    {
        if (_recipeModalMode == ModalModeEnum.Add)
            AddPlanifiedRecipe(selectedRecipe);
        else
            ChangePlanifiedRecipe(selectedRecipe);

        _isRecipeModalVisible = false;
    }

    private void AddPlanifiedRecipe(IRecipeModel selectedRecipe)
    {
        var planifiedDateTime = PlanifiedRecipeState.Value.CurrentWeekStart.StartOfWeek(DayOfWeek.Monday)
            .AddDays((int)_selectedDayForPlanning - 1)
            .Date;

        var planifiedRecipe = PlanifiedRecipeFactory.CreatePlanifiedRecipeModel(
            recipe: selectedRecipe,
            planifiedDatetime: planifiedDateTime,
            momentOftheDay: _selectedMomentOfTheDay.ToString(),
            userId: UserInfosState.Value.UserInfo.Id
        );

        Dispatcher.Dispatch(new AddItemAction<IPlanifiedRecipeModel>(planifiedRecipe));
    }

    private void ChangePlanifiedRecipe(IRecipeModel newRecipe)
    {
        if (_selectedPlanifiedRecipe is null) return;

        var newPlanifiedRecipe = PlanifiedRecipeFactory.CreatePlanifiedRecipeModel(
            recipe: newRecipe,
            planifiedDatetime: _selectedPlanifiedRecipe.PlanifiedDateTime,
            userId: _selectedPlanifiedRecipe.UserId,
            momentOftheDay: _selectedMomentOfTheDay.ToString(),
            recipeId: newRecipe.Id
        );

        if (!new PlanifiedRecipeEqualityComparer().Equals(_selectedPlanifiedRecipe, newPlanifiedRecipe))
        {
            Dispatcher.Dispatch(new AddItemAction<IPlanifiedRecipeModel>(newPlanifiedRecipe));

            Dispatcher.Dispatch(new DeleteItemAction<IPlanifiedRecipeModel>(_selectedPlanifiedRecipe));
        }
    }

    private IEnumerable<(DateTime Date, string FormattedDate)> GetOrderedDaysOfWeek()
    {
        var startOfWeek = PlanifiedRecipeState.Value.CurrentWeekStart;

        return Enumerable.Range(0, 7)
            .Select(i => startOfWeek.AddDays(i))
            .Select(date => (date, date.ToString("dddd dd MMMM yyyy", CultureInfo.CurrentCulture).ToUpper()))
            .OrderBy(tuple => tuple.date.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)tuple.date.DayOfWeek);
    }

    private List<OptionMenuItem> GetOptions()
    {
        List<OptionMenuItem> options = [
            new(MaterialIconsConstants.ShoppingCard, string.Empty, () => GenerateShoppingListAsync(), LabelsTranslator.GenerateShoppingList)
        ];

        if (!PlanifiedRecipeState.Value.RecipesGroupedByDay.Any(c => c.Value.Count() > 0) && FeatureManager.IsEnabledAsync(FeatureFlagsConstants.PlanifiedRecipesAutomaticaly).Result)
            options.Add(new(MaterialIconsConstants.Generate, string.Empty, () => PlanifiedRecipesForTheWeek(), LabelsTranslator.PlanifiedRecipesAutomaticaly));

        return options;
    }

    private async Task GenerateShoppingListAsync()
    {
        Dispatcher.Dispatch(new GenerateShoppingListAction(PlanifiedRecipeState.Value.Items.Select(c => c.RecipeModel), UserInfosState.Value.UserInfo.Id));

        await Task.CompletedTask;
    }

    private async Task PlanifiedRecipesForTheWeek()
    {
        Dispatcher.Dispatch(new PlanifiedRecipesForTheWeekAction(PlanifiedRecipeState.Value.CurrentWeekStart));

        await Task.CompletedTask;
    }

    private async Task PlanifiedRecipeAutomaticaly(IPlanifiedRecipeModel? planifiedRecipeModel = null, DateTime? day = null, string? momentOftheDay = null)
    {
        planifiedRecipeModel = planifiedRecipeModel ?? PlanifiedRecipeFactory.CreatePlanifiedRecipeModel(
            planifiedDatetime: day,
            userId: UserInfosState.Value.UserInfo.Id,
            momentOftheDay: momentOftheDay
        );

        Dispatcher.Dispatch(new PlanifiedRecipeAutomaticalyAction(planifiedRecipeModel));

        await Task.CompletedTask;
    }
}