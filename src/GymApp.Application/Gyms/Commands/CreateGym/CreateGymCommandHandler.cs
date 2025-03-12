using ErrorOr;
using GymApp.Application.Common.Interfaces;
using GymApp.Application.Rooms.Commands.CreateRoom;
using GymApp.Domain.GymAggregate;
using MediatR;

namespace GymApp.Application.Gyms.Commands.CreateGym;

public class CreateGymCommandHandler : IRequestHandler<CreateGymCommand, ErrorOr<Gym>>
{
    private readonly ISubscriptionsRepository _subscriptionsRepository;
    private readonly IGymsRepository _gymsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateGymCommandHandler(
        ISubscriptionsRepository subscriptionsRepository,
        IGymsRepository gymsRepository,
        IUnitOfWork unitOfWork)
    {
        _subscriptionsRepository = subscriptionsRepository;
        _gymsRepository = gymsRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task<ErrorOr<Gym>> Handle(CreateGymCommand command, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionsRepository.GetByIdAsync(command.SubscriptionId);

        if (subscription is null)
            return Error.NotFound($"Subscription with id {command.SubscriptionId} not found");

        var gym = new Gym(maxRooms: subscription.GetMaxRooms(), command.GymName, subscription.Id, Guid.NewGuid());

        var addGymResult = subscription.AddGym(gym);

        if (addGymResult.IsError)
            return addGymResult.Errors;

        await _gymsRepository.CreateAsync(gym);

        await _subscriptionsRepository.UpdateAsync(subscription);
        await _unitOfWork.CommitChangesAsync();

        return gym;
    }
}