using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using simple_recip_application.Data;
using simple_recip_application.Data.Persistence.Repositories;
using simple_recip_application.Dtos;
using simple_recip_application.Extensions;
using simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.HouseholdProductsManagement.Persistence.Entities;
using simple_recip_application.Features.TagsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.TagsManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.TagsManagement.Persistence.Entities;

namespace simple_recip_application.Features.TagsManagement.Persistence.Repositories;

public class TagRepository
(
    ApplicationDbContext _dbContext
)
: EntityBaseRepository<TagModel>(_dbContext), ITagRepository
{
    public new async Task<MethodResult<ITagModel?>> GetByIdAsync(Guid? id)
    {
        var result = await base.GetByIdAsync(id);

        return new MethodResult<ITagModel?>(result.Success, result.Item);
    }

    public new async Task<MethodResult<IEnumerable<ITagModel>>> GetAsync()
    {
        var result = await base.GetAsync();

        return new MethodResult<IEnumerable<ITagModel>>(result.Success, result.Item);
    }

    public async Task<MethodResult<IEnumerable<ITagModel>>> GetAsync(int take, int skip, Expression<Func<ITagModel, bool>>? predicate, Expression<Func<ITagModel, object>>? sort = null)
    {
        try
        {
            if (sort is null)
                sort = c => c.Name;

            var convertedPredicate = predicate?.Convert<ITagModel, TagModel, bool>();
            var convertedSort = sort?.Convert<ITagModel, TagModel, object>();

            var tagsRequest = base.Get(take, skip, convertedPredicate, convertedSort);

            var tags = await tagsRequest.Cast<ITagModel>().ToListAsync();

            return new MethodResult<IEnumerable<ITagModel>>(true, tags);
        }
        catch (Exception ex)
        {
            return new MethodResult<IEnumerable<ITagModel>>(false, []);
        }
    }

    public async Task<MethodResult> AddAsync(ITagModel? entity)
    {
        return await base.AddAsync(entity as TagModel);
    }

    public async Task<MethodResult> AddRangeAsync(IEnumerable<ITagModel>? entities)
    {
        return await base.AddRangeAsync(entities?.Cast<TagModel>() ?? []);
    }

    public async Task<MethodResult> UpdateAsync(ITagModel? entity)
    {
        return await base.UpdateAsync(entity as TagModel);
    }

    public async Task<MethodResult> DeleteAsync(ITagModel? entity)
    {
        return await base.DeleteAsync(entity as TagModel);
    }

    public async Task<MethodResult<int>> CountAsync(Expression<Func<ITagModel, bool>>? predicate = null)
    {
        var convertedPredicate = predicate?.Convert<ITagModel, TagModel, bool>();

        return await base.CountAsync(convertedPredicate);
    }
}
