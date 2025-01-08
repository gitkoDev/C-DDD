using ErrorOr;
using GymApp.Domain.Common;
using GymApp.Domain.Common.Entities;
using GymApp.Domain.SessionAggregate;
using Success = ErrorOr.Success;

namespace GymApp.Domain.TrainerAggregate;

public class Trainer(Guid? id = null) : AggregateRoot(id: id ?? Guid.NewGuid())
{
    private readonly Schedule _schedule = Schedule.Empty();

    private readonly Guid _userId;
    
    private readonly List<Guid> _sessionIds = [];
    
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