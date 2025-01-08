using GymApp.Domain.Common.ValueObjects;
using GymApp.Domain.SessionAggregate;
using GymApp.Domain.UnitTests.TestConstants;

namespace GymApp.Domain.UnitTests.TestUtils.Sessions;

public static class SessionFactory
{
    public static Session CreateSession(
        DateOnly? date = null,
        TimeRange? time = null,
        int maxParticipants = Constants.Session.MaxParticipants,
        Guid? id = null
        )
    {
        return new Session(
            date: date ?? Constants.Session.Date,
            time: time ?? Constants.Session.Time,
            maxParticipants: maxParticipants, 
            trainerId: Constants.Trainer.Id, 
            id: id ?? Constants.Session.Id
            );
    }
}