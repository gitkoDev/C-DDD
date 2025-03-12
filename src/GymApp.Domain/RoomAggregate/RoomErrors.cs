using ErrorOr;

namespace GymApp.Domain.RoomAggregate;

public static class RoomErrors
{
    public static readonly Error SessionExists = Error.Validation(
        "Room.SessionExists",
        description: "This room already has reservation for this session");
    
    public static readonly Error SessionsLimitExceeded = Error.Validation(
        code: "Room.SessionsLimitExceeded",
        description: "No more sessions can be scheduled, room is full for the day");
    
    public static readonly Error OverlappingSessions = Error.Conflict(
        code: "Room.OverlappingSessions",
        description: "Another session reservation in this room exists that overlaps with this one");
    
    public static Error RoomNotFound(Guid roomId)
    {
        return Error.NotFound("Room.RoomNotFound",
            description: $"Room with id {roomId} not found");
    }
}