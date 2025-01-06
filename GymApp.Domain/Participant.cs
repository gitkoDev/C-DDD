using ErrorOr;
using GymApp.Domain.Errors;
using Error = OneOf.Types.Error;

namespace GymApp.Domain;

public class Participant
{
    private readonly Guid _userId;
    private readonly List<Guid> _sessionIds = [];
    private readonly Schedule _schedule = Schedule.Empty();

    public Participant(Guid userId, Guid? id)
    {
        _userId = userId;
        Id = id ?? Guid.NewGuid();
    }

    public Guid Id { get; }

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