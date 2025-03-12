using GymApp.Domain.GymAggregate;
using GymApp.Domain.RoomAggregate;

namespace GymApp.Application.Common.Interfaces;

public interface IRoomsRepository
{
    Task CreateAsync(Room room);
    Task<Room?> GetByIdAsync(Guid roomId);
    Task<List<Room>> ListAsync(Guid gymId);
    Task DeleteAsync(Room room);
    Task UpdateAsync(Gym gym);
}