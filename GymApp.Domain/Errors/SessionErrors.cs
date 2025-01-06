using ErrorOr;

namespace GymApp.Domain.Errors;

public static class SessionErrors
{
    public static readonly Error ParticipantsLimitExceeded = Error.Validation(
        code: "Session.ParticipantsLimitExceeded",
        description: "No more participants can be added, session is full");

    public static readonly Error CancellationTooCloseToSessionTime = Error.Validation(
        code: "Session.CancellationTooCloseToSessionTime",
        description: "You cannot cancel within 24 hours of the session");

    public static readonly Error ReservationNotFound = Error.NotFound(
        "Session.ReservationNotFound",
        description: "This participant did not make a reservation");

    public static readonly Error OverlappingSessions = Error.Conflict(
        code: "Session.OverlappingSessions",
        description: "Another session reservation exists that overlaps with this one");
}