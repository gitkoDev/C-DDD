using ErrorOr;
using GymApp.Application.Common.Interfaces;
using GymApp.Domain.SubscriptionAggregate;
using MediatR;

namespace GymApp.Application.Subscriptions.Queries.ListSubscriptions;

public class ListSubscriptionsQueryHandler : IRequestHandler<ListSubscriptionsQuery, ErrorOr<List<Subscription>>>
{
    private readonly ISubscriptionsRepository _subscriptionsRepository;

    public ListSubscriptionsQueryHandler(ISubscriptionsRepository subscriptionsRepository)
    {
        _subscriptionsRepository = subscriptionsRepository;
    }
    
    public async Task<ErrorOr<List<Subscription>>> Handle(ListSubscriptionsQuery request, CancellationToken cancellationToken)
    {
        return await _subscriptionsRepository.ListAsync();
    }
}