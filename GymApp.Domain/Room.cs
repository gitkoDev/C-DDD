using ErrorOr;
using GymApp.Domain.Errors;

namespace GymApp.Domain;

public class Room
{
    public Guid Id { get; }
    private readonly List<Guid> _sessionIds = [];
    
    private readonly Schedule _schedule = Schedule.Empty();

    private readonly int _maxDailySessions;

    public Room(int maxDailySessions, Guid? id = null)
    {
        _maxDailySessions = maxDailySessions;
        Id = id ?? Guid.NewGuid();
    }

    public ErrorOr<Success> ScheduleSession(Session session)
    {
        if (_sessionIds.Any(id => id == session.Id))
            return RoomErrors.SessionExists;

        if (_sessionIds.Count >= _maxDailySessions)
            return RoomErrors.SessionsLimitExceeded;
        
        var bookTimeSlotResult = _schedule.BookTimeSlot(session.Date, session.Time);
        
        if (bookTimeSlotResult.IsError && bookTimeSlotResult.FirstError.Type == ErrorType.Conflict)
            return RoomErrors.OverlappingSessions;
        
        _sessionIds.Add(session.Id);
        
        return Result.Success;
    }
}