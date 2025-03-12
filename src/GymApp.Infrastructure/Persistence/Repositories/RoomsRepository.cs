using GymApp.Application.Common.Interfaces;
using GymApp.Domain.GymAggregate;
using GymApp.Domain.RoomAggregate;
using GymApp.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace GymApp.Infrastructure.Persistence.Repositories;

public class RoomsRepository : IRoomsRepository
{
    private readonly GymManagementDbContext _dbContext;

    public RoomsRepository(GymManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task CreateAsync(Room room)
    {
        await _dbContext.Rooms.AddAsync(room);
    }
    
    public async Task<Room?> GetByIdAsync(Guid roomId)
    {
        return await _dbContext.Rooms.FindAsync(roomId);
    }

    public async Task<List<Room>> ListAsync(Guid gymId)
    {
        return await _dbContext.Rooms
            .AsNoTracking()
            .Where(r => r.GymId == gymId)
            .ToListAsync();
    }

    public async Task DeleteAsync(Room room)
    {
        _dbContext.Rooms.Remove(room);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Gym gym)
    {
        _dbContext.Update(gym);
        await _dbContext.SaveChangesAsync();
    }
}