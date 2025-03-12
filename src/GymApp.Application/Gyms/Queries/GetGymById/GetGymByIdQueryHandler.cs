using ErrorOr;
using GymApp.Application.Common.Interfaces;
using GymApp.Domain.GymAggregate;
using MediatR;

namespace GymApp.Application.Gyms.Queries.GetGymById;

public class GetGymByIdQueryHandler : IRequestHandler<GetGymByIdQuery, ErrorOr<Gym>>
{
    private readonly ISubscriptionsRepository _subscriptionsRepository;
    private readonly IGymsRepository _gymsRepository;

    public GetGymByIdQueryHandler(
        ISubscriptionsRepository subscriptionsRepository,
        IGymsRepository gymsRepository)
    {
        _subscriptionsRepository = subscriptionsRepository;
        _gymsRepository = gymsRepository;
    }
    
    public async Task<ErrorOr<Gym>> Handle(GetGymByIdQuery query, CancellationToken cancellationToken)
    {
        if (await _subscriptionsRepository.ExistsAsync(query.SubscriptionId))
            return Error.NotFound($"Subscription with id {query.SubscriptionId} not found");
        
        var gym = await _gymsRepository.GetByIdAsync(query.GymId);
        
        return gym is null
            ? Error.NotFound(description: $"Gym with id {query.GymId} not found")
            : gym;
    }
}