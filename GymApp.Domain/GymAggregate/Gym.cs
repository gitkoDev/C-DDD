using ErrorOr;
using GymApp.Domain.Common;
using GymApp.Domain.RoomAggregate;

namespace GymApp.Domain.GymAggregate;

public class Gym(int maxRooms, Guid? id = null) : AggregateRoot(id: id ?? Guid.NewGuid())
{
    private readonly List<Guid> _roomIds = [];
    
    public ErrorOr<Success> AddRoom(Room room)
    {
        if (_roomIds.Contains(room.Id))
            return GymErrors.RoomExists;
        
        if (_roomIds.Count >= maxRooms)
            return GymErrors.RoomLimitExceeded;
        
        _roomIds.Add(room.Id);

        return Result.Success;
    }
}