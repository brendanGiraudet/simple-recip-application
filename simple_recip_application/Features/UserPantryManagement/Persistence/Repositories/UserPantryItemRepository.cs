using Microsoft.EntityFrameworkCore;
using simple_recip_application.Data;
using simple_recip_application.Dtos;
using simple_recip_application.Features.UserPantryManagement.ApplicationCore.Entities;
using simple_recip_application.Features.UserPantryManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.UserPantryManagement.Persistence.Entities;

namespace simple_recip_application.Features.UserPantryManagement.Persistence.Repositories;

public class UserPantryItemRepository(ApplicationDbContext _dbContext)
    : IUserPantryItemRepository
{
    public async Task<MethodResult<IEnumerable<IUserPantryItemModel>>> GetUserPantryItemsAsync(string userId)
    {
        try
        {
            var items = await _dbContext.Set<UserPantryItemModel>()
            .Where(pi => pi.UserId == userId)
            .ToListAsync();

            return new MethodResult<IEnumerable<IUserPantryItemModel>>(true, items);
        }
        catch (Exception ex)
        {

            return new MethodResult<IEnumerable<IUserPantryItemModel>>(false, []);
        }
    }

    public async Task<MethodResult<IUserPantryItemModel?>> GetUserPantryItemAsync(string userId, Guid productId)
    {
        try
        {
            var item = await _dbContext.Set<UserPantryItemModel>()
            .FirstOrDefaultAsync(pi => pi.UserId == userId && pi.ProductId == productId);

            return new MethodResult<IUserPantryItemModel?>(true, item);
        }
        catch (Exception ex)
        {
            return new MethodResult<IUserPantryItemModel?>(true, null);
        }
    }

    public async Task<MethodResult> AddAsync(IUserPantryItemModel pantryItem)
    {
        try
        {
            await _dbContext.Set<UserPantryItemModel>().AddAsync((UserPantryItemModel)pantryItem);

            await _dbContext.SaveChangesAsync();

            return new MethodResult(true);
        }
        catch (Exception ex)
        {
            return new MethodResult(false);
        }
    }

    public async Task<MethodResult> UpdateAsync(IUserPantryItemModel pantryItem)
    {
        try
        {
            _dbContext.Set<UserPantryItemModel>().Update((UserPantryItemModel)pantryItem);
            await _dbContext.SaveChangesAsync();

            return new MethodResult(true);
        }
        catch (Exception ex)
        {
            return new MethodResult(false);
        }
    }

    public async Task<MethodResult> DeleteAsync(IUserPantryItemModel pantryItem)
    {
        try
        {
            _dbContext.Set<UserPantryItemModel>().Remove((UserPantryItemModel)pantryItem);
            await _dbContext.SaveChangesAsync();

            return new MethodResult(true);
        }
        catch (Exception ex)
        {
            return new MethodResult(false);
        }
    }
}

