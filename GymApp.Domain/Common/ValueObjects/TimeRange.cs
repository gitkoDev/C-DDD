using ErrorOr;
using GymApp.Domain.SessionAggregate;
using OneOf.Types;
using Throw;

namespace GymApp.Domain.Common.ValueObjects;

public class TimeRange : ValueObject
{
    public TimeOnly Start { get; init; }
    
    public TimeOnly End { get; init; }

    public TimeRange(TimeOnly start, TimeOnly end)
    {
        Start = start.Throw().IfGreaterThanOrEqualTo(end);
        End = end;
    }

    public static ErrorOr<TimeRange> FromDateTimes(DateTime start, DateTime end)
    {
        if (start.Date != end.Date)
            return SessionErrors.StartAndEndNotOnSameDate;

        if (start <= end)
            return SessionErrors.StartTimeEarlierOrEqualToEndTime;

        return new TimeRange(
            start: TimeOnly.FromDateTime(start), 
            end: TimeOnly.FromDateTime(end));
    }

    public bool OverlapsWith(TimeRange anotherTimeRange)
    {
        if (Start >= anotherTimeRange.End) return false;
        if (End <= anotherTimeRange.Start) return false;

        return true;
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Start;
        yield return End;
    }
}