using ErrorOr;
using GymApp.Application.Common.Interfaces;
using GymApp.Domain.GymAggregate;
using GymApp.Domain.RoomAggregate;
using GymApp.Domain.SubscriptionAggregate;
using MediatR;

namespace GymApp.Application.Rooms.Commands.CreateRoom;

public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, ErrorOr<Room>>
{
    private readonly IGymsRepository _gymsRepository;
    private readonly ISubscriptionsRepository _subscriptionsRepository;
    private readonly IRoomsRepository _roomsRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateRoomCommandHandler(
        IGymsRepository gymsRepository,
        ISubscriptionsRepository subscriptionsRepository,
        IRoomsRepository roomsRepository, 
        IUnitOfWork unitOfWork)
    {
        _gymsRepository = gymsRepository;
        _subscriptionsRepository = subscriptionsRepository;
        _roomsRepository = roomsRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<ErrorOr<Room>> Handle(CreateRoomCommand command, CancellationToken cancellationToken)
    {
        var gym = await _gymsRepository.GetByIdAsync(command.GymId);
        if (gym is null)
            return GymErrors.GymNotFound(command.GymId);

        var subscription = await _subscriptionsRepository.GetByIdAsync(gym.SubscriptionId);
        if (subscription is null)
            return SubscriptionErrors.SubscriptionNotFound(command.GymId);

        var room = new Room(
            name: command.RoomName,
            maxDailySessions: subscription.GetMaxDailySessions(),
            gymId: command.GymId);

        var addRoomToGymResult = gym.AddRoom(room);
        if (addRoomToGymResult.IsError)
            return addRoomToGymResult.Errors;

        await _gymsRepository.UpdateAsync(gym);

        return room;
    }
}