using ErrorOr;
using GymApp.Domain.Common.ValueObjects;
using GymApp.Domain.SessionAggregate;

namespace GymApp.Domain.Common.Entities;

public class Schedule : Entity
{
    //EF
    private Schedule(){}
    
    public Schedule(
        Guid? id = null, Dictionary<DateOnly, List<TimeRange>>? calendar = null) : 
        base(id ?? Guid.NewGuid())
    {
        _calendar = calendar ?? new Dictionary<DateOnly, List<TimeRange>>();
    }
    
    private readonly Dictionary<DateOnly, List<TimeRange>> _calendar;

    public ErrorOr<Success> BookTimeSlot(DateOnly date, TimeRange time)
    {
        if (!_calendar.TryGetValue(date, out var timeSlots))
        {
            _calendar[date] = [time];
            return Result.Success;
        }

        if (timeSlots.Any(timeSlot => timeSlot.OverlapsWith(time)))
            return SessionErrors.OverlappingSessions;
        
        timeSlots.Add(time);
        
        return Result.Success;
    }

    public static Schedule Empty()
    {
        return new Schedule(id: Guid.NewGuid());
    }
}