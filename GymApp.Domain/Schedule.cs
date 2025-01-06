using ErrorOr;
using GymApp.Domain.Errors;

namespace GymApp.Domain;

public class Schedule
{
    private readonly Guid _id;

    private readonly Dictionary<DateOnly, List<TimeRange>> _calendar;

    public Schedule(Guid? id = null, Dictionary<DateOnly, List<TimeRange>>? calendar = null)
    {
        _id = id ?? Guid.NewGuid();
        _calendar = calendar ?? [];
    }

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