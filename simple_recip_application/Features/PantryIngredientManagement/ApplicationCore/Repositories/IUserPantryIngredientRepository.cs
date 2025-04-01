using simple_recip_application.Data.ApplicationCore.Repositories;
using simple_recip_application.Dtos;
using simple_recip_application.Features.PantryIngredientManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.PantryIngredientManagement.ApplicationCore.Repositories;

public interface IUserPantryIngredientRepository : IRepository<IUserPantryIngredientModel>
{
    Task<MethodResult<IEnumerable<IUserPantryIngredientModel>>> GetUserPantryIngredientsAsync(string userId);
    Task<MethodResult<IUserPantryIngredientModel?>> GetUserPantryIngredientAsync(string userId, Guid ingredientId);
}
