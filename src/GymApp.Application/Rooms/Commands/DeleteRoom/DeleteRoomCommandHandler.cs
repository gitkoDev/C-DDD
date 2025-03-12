using ErrorOr;
using GymApp.Application.Common.Interfaces;
using GymApp.Domain.GymAggregate;
using GymApp.Domain.RoomAggregate;
using MediatR;
using OneOf.Types;

namespace GymApp.Application.Rooms.Commands.DeleteRoom;

public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, ErrorOr<Deleted>>
{
    private readonly IGymsRepository _gymsRepository;
    private readonly IRoomsRepository _roomsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRoomCommandHandler(
        IGymsRepository gymsRepository,
        IRoomsRepository roomsRepository,
        IUnitOfWork unitOfWork)
    {
        _gymsRepository = gymsRepository;
        _roomsRepository = roomsRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<ErrorOr<Deleted>> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
    {
        var gym = await _gymsRepository.GetByIdAsync(request.GymId);
        if (gym is null)
            return GymErrors.GymNotFound(request.GymId);

        if (!gym.HasRoom(request.RoomId))
            return GymErrors.RoomInGymNotFound(request.RoomId);

        var room = await _roomsRepository.GetByIdAsync(request.RoomId);
        if (room is null)
            return RoomErrors.RoomNotFound(request.RoomId);
        
        var deleteRoomResult = gym.DeleteRoom(room);
        if (deleteRoomResult.IsError)
            return deleteRoomResult.Errors;

        await _gymsRepository.UpdateAsync(gym);

        return Result.Deleted;
    }
}