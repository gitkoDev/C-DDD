using GymApp.Domain.UnitTests.TestConstants;

namespace GymApp.Domain.UnitTests.Sessions;

public static class SessionFactory
{
    public static Session CreateSession(
        DateOnly? date = null,
        TimeOnly? startTime = null,
        TimeOnly? endTime = null,
        int maxParticipants = Constants.Session.MaxParticipants, 
        Guid? id = null
        )
    {
        return new Session(
            date: date ?? Constants.Session.Date,
            startTime: startTime ?? Constants.Session.StartTime,
            endTime: endTime ?? Constants.Session.EndTime,
            maxParticipants: maxParticipants, 
            trainerId: Constants.Trainer.Id, 
            id: id ?? Constants.Session.Id
            );
    }
}