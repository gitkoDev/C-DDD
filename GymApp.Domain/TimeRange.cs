using Throw;

namespace GymApp.Domain;

public class TimeRange
{
    public TimeOnly Start { get; init; }
    
    public TimeOnly End { get; init; }

    public TimeRange(TimeOnly start, TimeOnly end)
    {
        Start = start.Throw().IfGreaterThanOrEqualTo(end);
        End = end;
    }

    public bool OverlapsWith(TimeRange anotherTimeRange)
    {
        if (Start >= anotherTimeRange.End) return false;
        if (End <= anotherTimeRange.Start) return false;

        return true;
    }
}