using Microsoft.EntityFrameworkCore;
using simple_recip_application.Data;
using simple_recip_application.Data.Persistence.Repositories;
using simple_recip_application.Dtos;
using simple_recip_application.Extensions;
using simple_recip_application.Features.CalendarManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.CalendarUserAccessManagement.ApplicationCore.Entities;
using simple_recip_application.Features.CalendarUserAccessManagement.Persistence.Entities;
using System.Linq.Expressions;

namespace simple_recip_application.Features.CalendarManagement.Persistence.Repositories;

public class CalendarUserAccessRepository
(
    ApplicationDbContext _dbContext
)
: Repository<CalendarUserAccessModel>(_dbContext), ICalendarUserAccessRepository
{
    public new async Task<MethodResult<ICalendarUserAccessModel?>> GetByIdAsync(Guid? id)
    {
        try
        {
            var calendar = await _dbContext.Set<CalendarUserAccessModel>()
                                           .Where(c => c.CalendarId == id)
                                           .FirstOrDefaultAsync();

            return new MethodResult<ICalendarUserAccessModel?>(true, calendar);
        }
        catch (Exception ex)
        {
            return new MethodResult<ICalendarUserAccessModel?>(false, null);
        }
    }

    public new async Task<MethodResult<IEnumerable<ICalendarUserAccessModel>>> GetAsync()
    {
        var result = await base.GetAsync();

        return new MethodResult<IEnumerable<ICalendarUserAccessModel>>(result.Success, result.Item);
    }

    public async Task<MethodResult<IEnumerable<ICalendarUserAccessModel>>> GetAsync(int take, int skip, Expression<Func<ICalendarUserAccessModel, bool>>? predicate = null, Expression<Func<ICalendarUserAccessModel, object>>? sort = null)
    {
        try
        {
            if (sort is null)
                sort = c => c.UserEmail;

            var convertedPredicate = predicate?.Convert<ICalendarUserAccessModel, CalendarUserAccessModel, bool>();
            var convertedSort = sort?.Convert<ICalendarUserAccessModel, CalendarUserAccessModel, object>();

            var calendarsRequest = base.Get(take, skip, convertedPredicate, convertedSort)
                                       .Select(c => new CalendarUserAccessModel
                                       {
                                           UserEmail = c.UserEmail,
                                           UserId = c.UserId,
                                           CalendarId = c.CalendarId,
                                       });

            var calendars = await calendarsRequest.Cast<ICalendarUserAccessModel>().ToListAsync();

            return new MethodResult<IEnumerable<ICalendarUserAccessModel>>(true, calendars);
        }
        catch (Exception ex)
        {
            return new MethodResult<IEnumerable<ICalendarUserAccessModel>>(false, []);
        }
    }

    public async Task<MethodResult> AddAsync(ICalendarUserAccessModel? entity)
    {
        return await base.AddAsync(entity as CalendarUserAccessModel);
    }

    public async Task<MethodResult> AddRangeAsync(IEnumerable<ICalendarUserAccessModel>? entities)
    {
        return await base.AddRangeAsync(entities?.Cast<CalendarUserAccessModel>() ?? []);
    }

    public async Task<MethodResult> UpdateAsync(ICalendarUserAccessModel? entity)
    {
        if (entity?.CalendarId is null)
            return new MethodResult(false);

        // Récupération complète du calendrier
        var actualCalendarResult = await GetByIdAsync(entity.CalendarId);
        if (!actualCalendarResult.Success || actualCalendarResult.Item is null)
            return new MethodResult(false);

        var existingCalendar = actualCalendarResult.Item;

        existingCalendar.UserEmail = entity.UserEmail;
        existingCalendar.UserId = entity.UserId;

        // Sauvegarde finale des modifications
        await _dbContext.SaveChangesAsync();

        return new MethodResult(true);
    }

    public async Task<MethodResult> UpdateRangeAsync(IEnumerable<ICalendarUserAccessModel>? entities)
    {
        return await base.UpdateRangeAsync(entities as IEnumerable<CalendarUserAccessModel>);
    }

    public async Task<MethodResult> DeleteAsync(ICalendarUserAccessModel? entity)
    {
        return await base.DeleteAsync(entity as CalendarUserAccessModel);
    }

    public async Task<MethodResult> DeleteRangeAsync(IEnumerable<ICalendarUserAccessModel>? entities)
    {
        return await base.DeleteRangeAsync(entities as IEnumerable<CalendarUserAccessModel>);
    }

    public async Task<MethodResult<int>> CountAsync(Expression<Func<ICalendarUserAccessModel, bool>>? predicate = null)
    {
        var convertedPredicate = predicate?.Convert<ICalendarUserAccessModel, CalendarUserAccessModel, bool>();

        return await CountAsync(convertedPredicate);
    }
}
