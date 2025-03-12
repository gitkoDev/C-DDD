using GymApp.Domain.Common;

namespace GymApp.Domain.SessionAggregate;

public class Reservation(Guid participantId, Guid? id = null) : Entity(id: id ?? Guid.NewGuid())
{
    public Guid ParticipantId { get; } = participantId;
}