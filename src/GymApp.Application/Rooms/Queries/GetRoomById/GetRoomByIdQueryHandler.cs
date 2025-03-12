using ErrorOr;
using GymApp.Application.Common.Interfaces;
using GymApp.Domain.GymAggregate;
using GymApp.Domain.RoomAggregate;
using MediatR;

namespace GymApp.Application.Rooms.Queries.GetRoomById;

public class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery, ErrorOr<Room>>
{
    private readonly IGymsRepository _gymsRepository;
    private readonly IRoomsRepository _roomsRepository;

    public GetRoomByIdQueryHandler(
        IGymsRepository gymsRepository,
        IRoomsRepository roomsRepository
    )
    {
        _gymsRepository = gymsRepository;
        _roomsRepository = roomsRepository;
    }

    public async Task<ErrorOr<Room>> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
    {
        var gym = await _gymsRepository.GetByIdAsync(request.GymId);
        if (gym is null)
            return GymErrors.GymNotFound(request.GymId);

        if (!gym.HasRoom(request.RoomId))
            return GymErrors.RoomInGymNotFound(request.GymId);

        var room = await _roomsRepository.GetByIdAsync(request.RoomId);
        if (room is null)
            return RoomErrors.RoomNotFound(request.RoomId);

        return room;
    }
}