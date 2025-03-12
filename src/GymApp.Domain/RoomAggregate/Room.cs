using ErrorOr;
using GymApp.Domain.Common;
using GymApp.Domain.Common.Entities;
using GymApp.Domain.SessionAggregate;

namespace GymApp.Domain.RoomAggregate;

public class Room : AggregateRoot
{
    private readonly List<Guid> _sessionIds = [];
    private readonly Dictionary<DateOnly, List<Guid>> _sessionsIdsByDate = [];
    private readonly int _maxDailySessions;
    private readonly Schedule _schedule = Schedule.Empty();
    
    private IReadOnlyCollection<Guid> SessionsIds => _sessionsIdsByDate.Values
        .SelectMany(sessionsIds => sessionsIds)
        .ToList()
        .AsReadOnly();
    
    public string Name { get; }
    
    public Guid GymId { get; }


    public Room(
        string name,
        int maxDailySessions,
        Guid gymId,
        Schedule? schedule = null,
        Guid? id = null) : base(id: id ?? Guid.NewGuid())
    {
        Name = name;
        GymId = gymId;
        _schedule = schedule ?? Schedule.Empty();
        _maxDailySessions = maxDailySessions;
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
    
    private Room(){}
}