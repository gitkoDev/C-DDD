using ErrorOr;
using GymApp.Domain.Errors;

namespace GymApp.Domain;

public class Gym
{
    private readonly List<Guid> _roomIds = [];
    public Guid Id { get; }

    private readonly int _maxRooms;

    public Gym(int maxRooms, Guid? id = null)
    {
        _maxRooms = maxRooms;
        Id = id ?? Guid.NewGuid();
    }

    public ErrorOr<Success> AddRoom(Room room)
    {
        if (_roomIds.Contains(room.Id))
            return GymErrors.RoomExists;
        
        if (_roomIds.Count >= _maxRooms)
            return GymErrors.RoomLimitExceeded;
        
        _roomIds.Add(room.Id);

        return Result.Success;
    }
}