using Fluxor;
using Microsoft.AspNetCore.Components;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.PantryIngredientManagement.ApplicationCore.Entities;
using simple_recip_application.Features.PantryIngredientManagement.ApplicationCore.Factories;
using simple_recip_application.Features.PantryIngredientManagement.Store;
using simple_recip_application.Features.PantryIngredientManagement.Store.Actions;
using simple_recip_application.Features.UserInfos.Store;

namespace simple_recip_application.Features.PantryIngredientManagement.UserInterfaces.Pages.PantryIngredients;

public partial class PantryIngredients
{
    [Inject] public required IState<UserInfosState> UserInfosState { get; set; }
    [Inject] public required IState<UserPantryIngredientState> UserPantryIngredientState { get; set; }
    [Inject] public required IUserPantryIngredientFactory PantryIngredientFactory { get; set; }
    [Inject] public required IDispatcher Dispatcher { get; set; }

    private bool _isAddIngredientModalVisible = false;
    private IIngredientModel? _selectedIngredient;
    private decimal _selectedQuantity = 1;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        if(UserInfosState.Value.UserInfo?.Id != null)
            Dispatcher.Dispatch(new LoadUserPantryIngredientsAction(UserInfosState.Value.UserInfo.Id));
    }

    private void OpenAddIngredientModal()
    {
        _isAddIngredientModalVisible = true;
        _selectedQuantity = 1;
        _selectedIngredient = null;
    }

    private void CloseAddIngredientModal(bool _) => _isAddIngredientModalVisible = false;

    private void HandleIngredientSelected(IIngredientModel ingredient)
    {
        _selectedIngredient = ingredient;
    }

    private void AddIngredientAsync()
    {
        if (_selectedIngredient is null)
            return;

        var pantryIngredient = PantryIngredientFactory.Create(
            userId: UserInfosState.Value.UserInfo.Id,
            ingredient: _selectedIngredient,
            quantity: _selectedQuantity
        );

        Dispatcher.Dispatch(new AddOrUpdateUserPantryIngredientAction(pantryIngredient));
        _isAddIngredientModalVisible = false;
    }

    private void DeleteIngredientAsync(IUserPantryIngredientModel ingredient)
    {
        Dispatcher.Dispatch(new DeleteUserPantryIngredientAction(ingredient));
    }

    private string GetIngredientsVisibilityCssClass() => !UserPantryIngredientState.Value.IsLoading ? "" : "hidden";
}
