using ErrorOr;
using GymApp.Domain.SubscriptionAggregate;

namespace GymApp.Application.Common.Interfaces;

public interface ISubscriptionsRepository
{
    Task CreateAsync (Subscription subscription);
    Task<List<Subscription>> ListAsync();
    Task<Subscription?> GetByIdAsync(Guid subscriptionId);
    Task UpdateAsync(Subscription subscription);
    Task RemoveAsync(Subscription subscription);
    Task<bool> ExistsAsync(Guid subscriptionId);
}