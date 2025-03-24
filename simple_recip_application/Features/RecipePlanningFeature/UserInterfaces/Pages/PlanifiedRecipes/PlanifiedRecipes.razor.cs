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
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Services;
using simple_recip_application.Features.RecipePlanningFeature.Enums;
using simple_recip_application.Resources;
using simple_recip_application.Store.Actions;
using simple_recip_application.Enums;

namespace simple_recip_application.Features.RecipePlanningFeature.UserInterfaces.Pages.PlanifiedRecipes;


public partial class PlanifiedRecipes
{
    [Inject] public required ILogger<PlanifiedRecipes> Logger { get; set; }
    [Inject] public required IState<PlanifiedRecipeState> PlanifiedRecipeState { get; set; }
    [Inject] public required IDispatcher Dispatcher { get; set; }
    [Inject] public required IPlanifiedRecipeModelFactory PlanifiedRecipeFactory { get; set; }
    [Inject] public required NavigationManager NavigationManager { get; set; }
    [Inject] public required IShoppingListGeneratorService ShoppingListGenerator { get; set; }
    [Inject] public required INotificationMessageFactory NotificationMessageFactory { get; set; }
    [Inject] public required IJSRuntime JSRuntime { get; set; }

    private bool _isRecipeModalVisible = false;
    private ModalModeEnum _recipeModalMode;

    private DayOfWeek _selectedDayForPlanning;
    private IPlanifiedRecipeModel? _selectedPlanifiedRecipe;
    private MomentOfTheDayEnum _selectedMomentOfTheDay = MomentOfTheDayEnum.Evening;

    private IJSObjectReference? _module;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await OnAfterRenderAsync(firstRender);

        if (firstRender)
            _module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/download.js");
    }

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
            userId: "currentUserId"
        );

        Dispatcher.Dispatch(new AddItemAction<IPlanifiedRecipeModel>(planifiedRecipe));
    }

    private void ChangePlanifiedRecipe(IRecipeModel newRecipe)
    {
        if (_selectedPlanifiedRecipe is null) return;

        var updatedPlanifiedRecipe = PlanifiedRecipeFactory.CreatePlanifiedRecipeModel(
            recipe: newRecipe,
            planifiedDatetime: _selectedPlanifiedRecipe.PlanifiedDateTime,
            userId: _selectedPlanifiedRecipe.UserId,
            momentOftheDay: _selectedMomentOfTheDay.ToString(),
            recipeId: newRecipe.Id
        );

        Dispatcher.Dispatch(new DeleteItemAction<IPlanifiedRecipeModel>(_selectedPlanifiedRecipe));
        Dispatcher.Dispatch(new AddItemAction<IPlanifiedRecipeModel>(updatedPlanifiedRecipe));
    }

    private IEnumerable<(DayOfWeek DayOfWeek, string FormattedDate)> GetOrderedDaysOfWeek()
    {
        var startOfWeek = PlanifiedRecipeState.Value.CurrentWeekStart;

        return Enumerable.Range(0, 7)
            .Select(i => startOfWeek.AddDays(i))
            .Select(date => (date.DayOfWeek, date.ToString("dddd dd MMMM yyyy", CultureInfo.CurrentCulture).ToUpper()))
            .OrderBy(tuple => tuple.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)tuple.DayOfWeek);
    }

    private List<OptionMenuItem> GetOptions() => [
        new("shopping_cart_checkout", string.Empty, () => GenerateCsvAsync(), LabelsTranslator.GenerateShoppingList)
    ];

    private async Task GenerateCsvAsync()
    {
        try
        {
            var result = await ShoppingListGenerator.GenerateShoppingListCsvContentAsync(PlanifiedRecipeState.Value.Items.Select(c => c.RecipeModel));

            if (result.Success && string.IsNullOrEmpty(result.Item))
            {
                var notification = NotificationMessageFactory.CreateNotificationMessage(MessagesTranslator.GenerateShoppingListCsvContentErrorMessage, NotificationsManagement.ApplicationCore.Enums.NotificationType.Error);

                Dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

                return;
            }

            var csvBytes = Encoding.UTF8.GetBytes(result.Item);

            var csvDataUrl = $"data:text/csv;base64,{Convert.ToBase64String(csvBytes)}";

            var filename = "ingredients.csv";

            if (_module is not null)
                await _module.InvokeVoidAsync("triggerFileDownload", filename, csvDataUrl);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, $"Error during GenerateCsvAsync: {ex.Message}");
        }
    }
}