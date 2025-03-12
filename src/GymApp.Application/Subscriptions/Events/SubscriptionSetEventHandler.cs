using GymApp.Application.Common.Interfaces;
using GymApp.Domain.AdminAggregate.Events;
using MediatR;

namespace GymApp.Application.Subscriptions.Events;

public class SubscriptionSetEventHandler : INotificationHandler<SubscriptionSetEvent>
{
    private readonly ISubscriptionsRepository _subscriptionsRepository;

    public SubscriptionSetEventHandler(ISubscriptionsRepository subscriptionsRepository)
    {
        _subscriptionsRepository = subscriptionsRepository;
    }

    public Task Handle(SubscriptionSetEvent @event, CancellationToken cancellationToken)
    {
        _subscriptionsRepository.CreateAsync(@event.Subscription);
        
        return Task.CompletedTask;
    }
}