using ErrorOr;

namespace GymApp.Domain.GymAggregate;

public static class GymErrors
{
    public static readonly Error RoomExists = Error.Conflict(
        "Gym.RoomExists",
        description: "This room already exists in the session");
    
    public static readonly Error RoomLimitExceeded = Error.Validation(
        code: "Gym.RoomsLimitExceeded",
        description: "No more rooms can be added, limit exceeded");
}