using ErrorOr;

namespace GymApp.Domain.Errors;

public class TrainerErrors
{
    public static readonly Error OverlappingSessions = Error.Validation(
        code: "Trainer.OverlappingSessions",
        description: "You cannot teach several sessions that happen at the same time");

    public static readonly Error SessionExists = Error.Conflict(
        code: "Trainer.SessionExists",
        description: "Trainer already has reservation for this session");
}