using GymApp.Domain.ParticipantAggregate;
using GymApp.Domain.UnitTests.TestConstants;

namespace GymApp.Domain.UnitTests.TestUtils.Participants;

public class ParticipantFactory
{
    public static Participant CreateParticipant(Guid? id = null, Guid? userId = null)
    {
        return new Participant(userId: userId ?? Constants.User.Id, id: id ?? Constants.Participant.Id);
    }
}