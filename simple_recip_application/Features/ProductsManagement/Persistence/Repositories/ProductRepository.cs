using System.Linq.Expressions;
using simple_recip_application.Data;
using simple_recip_application.Data.Persistence.Repositories;
using simple_recip_application.Dtos;
using simple_recip_application.Extensions;
using simple_recip_application.Features.ProductsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.ProductsManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.ProductsManagement.Persistence.Entities;

namespace simple_recip_application.Features.ProductsManagement.Persistence.Repositories;

public class ProductRepository
(
    ApplicationDbContext _dbContext
)
 : EntityBaseRepository<ProductModel>(_dbContext), IProductRepository
{
    public new async Task<MethodResult<IProductModel?>> GetByIdAsync(Guid? id)
    {
        var result = await base.GetByIdAsync(id);

        return new MethodResult<IProductModel?>(result.Success, result.Item);
    }

    public new async Task<MethodResult<IEnumerable<IProductModel>>> GetAsync()
    {
        var result = await base.GetAsync();

        return new MethodResult<IEnumerable<IProductModel>>(result.Success, result.Item);
    }

    public async Task<MethodResult<IEnumerable<IProductModel>>> GetAsync(int take, int skip, Expression<Func<IProductModel, bool>>? predicate)
    {
        var convertedPredicate = predicate?.Convert<IProductModel, ProductModel, bool>();
        
        var result = await base.GetAsync(take, skip, convertedPredicate);

        return new MethodResult<IEnumerable<IProductModel>>(result.Success, result.Item.OrderBy(c => c.Name).Cast<IProductModel>());
    }

    public async Task<MethodResult> AddAsync(IProductModel? entity)
    {
        return await base.AddAsync(entity as ProductModel);
    }

    public async Task<MethodResult> AddRangeAsync(IEnumerable<IProductModel>? entities)
    {
        return await base.AddRangeAsync(entities?.Cast<ProductModel>() ?? []);
    }

    public async Task<MethodResult> UpdateAsync(IProductModel? entity)
    {
        return await base.UpdateAsync(entity as ProductModel);
    }

    public async Task<MethodResult> DeleteAsync(IProductModel? entity)
    {
        return await base.DeleteAsync(entity as ProductModel);
    }
}
