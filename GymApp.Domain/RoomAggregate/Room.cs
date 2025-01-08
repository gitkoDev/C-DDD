using ErrorOr;
using GymApp.Domain.Common;
using GymApp.Domain.Common.Entities;
using GymApp.Domain.SessionAggregate;

namespace GymApp.Domain.RoomAggregate;

public class Room(int maxDailySessions, Guid? id = null) : AggregateRoot(id: id ?? Guid.NewGuid())
{
    private readonly List<Guid> _sessionIds = [];
    
    private readonly Schedule _schedule = Schedule.Empty();

    public ErrorOr<Success> ScheduleSession(Session session)
    {
        if (_sessionIds.Any(id => id == session.Id))
            return RoomErrors.SessionExists;

        if (_sessionIds.Count >= maxDailySessions)
            return RoomErrors.SessionsLimitExceeded;
        
        var bookTimeSlotResult = _schedule.BookTimeSlot(session.Date, session.Time);
        
        if (bookTimeSlotResult.IsError && bookTimeSlotResult.FirstError.Type == ErrorType.Conflict)
            return RoomErrors.OverlappingSessions;
        
        _sessionIds.Add(session.Id);
        
        return Result.Success;
    }
}