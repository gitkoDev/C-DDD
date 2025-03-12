using ErrorOr;
using GymApp.Domain.Common;
using GymApp.Domain.Common.Entities;
using GymApp.Domain.SessionAggregate;

namespace GymApp.Domain.ParticipantAggregate;

public class Participant(Guid userId, Guid? id) : AggregateRoot(id: id ?? Guid.NewGuid())
{
    private readonly Guid _userId = userId;
    private readonly List<Guid> _sessionIds = [];
    private readonly Schedule _schedule = Schedule.Empty();

    public ErrorOr<Success> AddToSchedule(Session session)
    {
        if (_sessionIds.Contains(session.Id))
            return ParticipantErrors.SessionExists;

        var bookTimeSlotResult = _schedule.BookTimeSlot(session.Date, session.Time);

        if (bookTimeSlotResult.IsError && bookTimeSlotResult.FirstError == SessionErrors.OverlappingSessions)
            return ParticipantErrors.OverlappingSessions;
        
        _sessionIds.Add(session.Id);
        return Result.Success;
    }
}