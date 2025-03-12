using ErrorOr;
using GymApp.Application.Common.Interfaces;
using GymApp.Domain.SubscriptionAggregate;
using MediatR;

namespace GymApp.Application.Subscriptions.Queries.GetSubscriptionById;

public class GetSubscriptionByIdQueryHandler : IRequestHandler<GetSubscriptionByIdQuery, ErrorOr<Subscription>>
{
    private readonly ISubscriptionsRepository _subscriptionsRepository;

    public GetSubscriptionByIdQueryHandler(ISubscriptionsRepository subscriptionsRepository)
    {
        _subscriptionsRepository = subscriptionsRepository;
    }
    
    public async Task<ErrorOr<Subscription>> Handle(GetSubscriptionByIdQuery query,
        CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionsRepository.GetByIdAsync(query.SubscriptionId);
        
        return subscription is null
            ? Error.NotFound(description: $"Subscription with id {query.SubscriptionId} not found")
            : subscription;
    }
    
}