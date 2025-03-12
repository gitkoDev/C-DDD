using GymApp.Application.Common.Interfaces;
using GymApp.Domain.GymAggregate;
using GymApp.Domain.TrainerAggregate;
using GymApp.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace GymApp.Infrastructure.Persistence.Repositories;

public class GymsRepository : IGymsRepository
{
    private readonly GymManagementDbContext _dbContext;

    public GymsRepository(GymManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task CreateAsync(Gym gym)
    {
        await _dbContext.Gyms.AddAsync(gym);
    }
    
    public async Task<List<Gym>> ListAsync(Guid subscriptionId)
    {
        return await _dbContext.Gyms
            .Where(gym => gym.SubscriptionId == subscriptionId)
            .ToListAsync();
    }
    
    public async Task RemoveAsync(Gym gym)
    {
        _dbContext.Gyms.Remove(gym);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveRangeAsync(List<Gym> gyms)
    {
        _dbContext.Gyms.RemoveRange(gyms);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Gym?> GetByIdAsync(Guid gymId)
    {
        return await _dbContext.Gyms.FindAsync(gymId);
    }

    public async Task UpdateAsync(Gym gym)
    {
        _dbContext.Update(gym);
        await _dbContext.SaveChangesAsync();
    }
}