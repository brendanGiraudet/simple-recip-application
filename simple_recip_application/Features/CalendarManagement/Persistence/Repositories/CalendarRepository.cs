using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using simple_recip_application.Data;
using simple_recip_application.Data.Persistence.Repositories;
using simple_recip_application.Dtos;
using simple_recip_application.Extensions;
using simple_recip_application.Features.CalendarManagement.ApplicationCore.Entities;
using simple_recip_application.Features.CalendarManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.CalendarManagement.Persistence.Entities;

namespace simple_recip_application.Features.CalendarManagement.Persistence.Repositories;

public class CalendarRepository
(
    ApplicationDbContext _dbContext
)
: EntityBaseRepository<CalendarModel>(_dbContext), ICalendarRepository
{
    public new async Task<MethodResult<ICalendarModel?>> GetByIdAsync(Guid? id)
    {
        try
        {
            var calendar = await _dbContext.Set<CalendarModel>()
                                         .Where(c => c.Id == id)
                                         .Include(c => c.PlanifiedRecipes)
                                            .ThenInclude(c => c.Recipe)
                                         .Include(c => c.CalendarUsersAccess)
                                         .FirstOrDefaultAsync();

            return new MethodResult<ICalendarModel?>(true, calendar);
        }
        catch (Exception ex)
        {
            return new MethodResult<ICalendarModel?>(false, null);
        }
    }

    public new async Task<MethodResult<IEnumerable<ICalendarModel>>> GetAsync()
    {
        var result = await base.GetAsync();

        return new MethodResult<IEnumerable<ICalendarModel>>(result.Success, result.Item);
    }

    public async Task<MethodResult<IEnumerable<ICalendarModel>>> GetAsync(int take, int skip, Expression<Func<ICalendarModel, bool>>? predicate = null, Expression<Func<ICalendarModel, object>>? sort = null)
    {
        try
        {
            if (sort is null)
                sort = c => c.Name;

            var convertedPredicate = predicate?.Convert<ICalendarModel, CalendarModel, bool>();
            var convertedSort = sort?.Convert<ICalendarModel, CalendarModel, object>();

            var calendarsRequest = base.Get(take, skip, convertedPredicate, convertedSort)
                                       .Include(c => c.CalendarUsersAccess)
                                       .Select(c => new CalendarModel
                                       {
                                           Id = c.Id,
                                           Name = c.Name,
                                       });

            var calendars = await calendarsRequest.Cast<ICalendarModel>().ToListAsync();

            return new MethodResult<IEnumerable<ICalendarModel>>(true, calendars);
        }
        catch (Exception ex)
        {
            return new MethodResult<IEnumerable<ICalendarModel>>(false, []);
        }
    }

    public async Task<MethodResult> AddAsync(ICalendarModel? entity)
    {
        return await base.AddAsync(entity as CalendarModel);
    }

    public async Task<MethodResult> AddRangeAsync(IEnumerable<ICalendarModel>? entities)
    {
        return await base.AddRangeAsync(entities?.Cast<CalendarModel>() ?? []);
    }

    public async Task<MethodResult> UpdateAsync(ICalendarModel? entity)
    {
        if (entity?.Id is null)
            return new MethodResult(false);

        // Récupération complète du calendrier
        var actualCalendarResult = await GetByIdAsync(entity.Id);
        if (!actualCalendarResult.Success || actualCalendarResult.Item is null)
            return new MethodResult(false);

        var existingCalendar = actualCalendarResult.Item;

        existingCalendar.Name = entity.Name;

        // Sauvegarde finale des modifications
        await _dbContext.SaveChangesAsync();

        return new MethodResult(true);
    }

    public async Task<MethodResult> UpdateRangeAsync(IEnumerable<ICalendarModel>? entities)
    {
        return await base.UpdateRangeAsync(entities as IEnumerable<CalendarModel>);
    }

    public async Task<MethodResult> DeleteAsync(ICalendarModel? entity)
    {
        return await base.DeleteAsync(entity as CalendarModel);
    }

    public async Task<MethodResult> DeleteRangeAsync(IEnumerable<ICalendarModel>? entities)
    {
        return await base.DeleteRangeAsync(entities as IEnumerable<CalendarModel>);
    }

    public async Task<MethodResult<int>> CountAsync(Expression<Func<ICalendarModel, bool>>? predicate = null)
    {
        var convertedPredicate = predicate?.Convert<ICalendarModel, CalendarModel, bool>();

        return await CountAsync(convertedPredicate);
    }
}
