using ErrorOr;

namespace GymApp.Domain.GymAggregate;

public static class GymErrors
{
    public static readonly Error TrainerExists = Error.Conflict(
        "Gym.TrainerExists",
        description: "This trainer already exists in the gym");
    
    public static readonly Error RoomExists = Error.Conflict(
        "Gym.RoomExists",
        description: "This room already exists in the gym");
    
    public static readonly Error RoomLimitExceeded = Error.Validation(
        code: "Gym.RoomsLimitExceeded",
        description: "No more rooms can be added, limit exceeded");
    
    public static Error GymNotFound(Guid gymId)
    {
        return Error.NotFound("Gym.GymNotFound",
            description: $"Gym with id {gymId} not found");
    }
    
    public static Error RoomInGymNotFound(Guid roomId)
    {
        return Error.NotFound("Gym.RoomInGymNotFound",
            description: $"Room with id {roomId} not found in the gym");
    }
}