using ErrorOr;
using GymApp.Domain.Common;
using GymApp.Domain.RoomAggregate;
using GymApp.Domain.TrainerAggregate;

namespace GymApp.Domain.GymAggregate;

public class Gym : AggregateRoot
{
    //EF
    private Gym(){}
    
    private readonly int _maxRooms;
    private readonly List<Guid> _roomIds = [];
    private readonly List<Guid> _trainerIds = [];
    
    public string Name { get; }
    
    public Guid SubscriptionId { get; }

    public IReadOnlyList<Guid> RoomsIds => _roomIds;
    
    public Gym(int maxRooms, string name, Guid subscriptionId, Guid? id = null) : base(id: id ?? Guid.NewGuid())
    {
        _maxRooms = maxRooms;
        Name = name;
        SubscriptionId = subscriptionId;
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

    public ErrorOr<Success> AddTrainer(Trainer trainer)
    {
        if (_trainerIds.Contains(trainer.Id))
            return GymErrors.TrainerExists;
        
        _trainerIds.Add(trainer.Id);

        return Result.Success;
    }

    public bool HasRoom(Guid roomId)
    {
        return _roomIds.Contains(roomId);
    }

    public ErrorOr<Deleted> DeleteRoom(Room room)
    {
        if (!_roomIds.Contains(room.Id))
            return GymErrors.RoomInGymNotFound(room.Id);

        _roomIds.Remove(room.Id);

        return Result.Deleted;
    }

}