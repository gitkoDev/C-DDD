using ErrorOr;
using GymApp.Domain.Errors;
using OneOf.Types;
using Success = ErrorOr.Success;

namespace GymApp.Domain;

public class Trainer
{
    private readonly Schedule _schedule = Schedule.Empty();
    
    private readonly Guid _id;
    private readonly Guid _userId;
    private readonly List<Guid> _sessionIds = [];

    public Trainer(Guid id)
    {
        _id = id;
    }


    public ErrorOr<Success> AddSessionToSchedule(Session session)
    {
        if (_sessionIds.Any(id => id == session.Id))
            return TrainerErrors.SessionExists;
        
        var bookTimeSlotResult = _schedule.BookTimeSlot(session.Date, session.Time);
        
        if (bookTimeSlotResult.IsError && bookTimeSlotResult.FirstError.Type == ErrorType.Conflict)
            return TrainerErrors.OverlappingSessions;
        
        _sessionIds.Add(session.Id);
        
        return Result.Success;
    }
}