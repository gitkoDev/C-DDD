using GymApp.Application.Common.Interfaces;
using GymApp.Domain.SubscriptionAggregate;
using GymApp.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace GymApp.Infrastructure.Persistence.Repositories;

public class SubscriptionsRepository : ISubscriptionsRepository
{
    private readonly GymManagementDbContext _dbContext;

    public SubscriptionsRepository(GymManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task CreateAsync(Subscription subscription)
    {
        await _dbContext.Subscriptions.AddAsync(subscription);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<List<Subscription>> ListAsync()
    {
        return await _dbContext.Subscriptions.ToListAsync();
    }
    
    public async Task<Subscription?> GetByIdAsync(Guid subscriptionId)
    {
        return await _dbContext.Subscriptions.FindAsync(subscriptionId);
    }

    public async Task UpdateAsync(Subscription subscription)
    {
        _dbContext.Update(subscription);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(Subscription subscription)
    {
        _dbContext.Remove(subscription);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid subscriptionId)
    {
        return await _dbContext.Subscriptions
            .AsNoTracking()
            .AnyAsync(subscription => subscription.Id == subscriptionId);
    }
}