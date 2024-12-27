using ErrorOr;

namespace GymApp.Domain;

public static class SessionErrors
{
    public static Error ParticipantsLimitExceeded = Error.Validation(
        code: "Session.ParticipantsLimitExceeded",
        description: "Participants limit exceeded");

    public static Error CancellationTooCloseToSessionTime = Error.Validation(
        code: "Session.CancellationTooCloseToSessionTime",
        description: "You cannot cancel within 24 hours of the session");

    public static Error ReservationNotFound = Error.Validation(
        "Session.ReservationNotFound",
        description: "This participant did not make a reservation");
}