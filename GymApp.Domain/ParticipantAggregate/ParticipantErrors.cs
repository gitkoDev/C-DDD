using ErrorOr;

namespace GymApp.Domain.ParticipantAggregate;

public static class ParticipantErrors
{
    public static readonly Error OverlappingSessions = Error.Validation(
        code: "Participant.OverlappingSessions",
        description: "You cannot reserve several sessions that happen at the same time");

    public static readonly Error SessionExists = Error.Conflict(
        code: "Participant.SessionExists",
        description: "This session reservation already exists in the participant schedule");
    
    public static readonly Error SessionForThisTimeExists = Error.Conflict(
        code: "Participant.SessionForThisTimeExists",
        description: "This participant already has another reservation for the same time");
}