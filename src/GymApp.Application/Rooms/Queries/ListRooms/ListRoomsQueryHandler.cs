using ErrorOr;
using GymApp.Application.Common.Interfaces;
using GymApp.Application.Subscriptions.Queries.ListSubscriptions;
using GymApp.Domain.GymAggregate;
using GymApp.Domain.RoomAggregate;
using MediatR;

namespace GymApp.Application.Rooms.Queries.ListRooms;

public class ListRoomsQueryHandler : IRequestHandler<ListRoomsQuery, ErrorOr<List<Room>>>
{
    private readonly IGymsRepository _gymsRepository;
    private readonly IRoomsRepository _roomsRepository;

    public ListRoomsQueryHandler(
        IGymsRepository gymsRepository,
        IRoomsRepository roomsRepository
    )
    {
        _gymsRepository = gymsRepository;
        _roomsRepository = roomsRepository;
    }
    
    public async Task<ErrorOr<List<Room>>> Handle(ListRoomsQuery request, CancellationToken cancellationToken)
    {
        var gym = await _gymsRepository.GetByIdAsync(request.GymId);

        if (gym is null)
            return GymErrors.GymNotFound(request.GymId);

        return await _roomsRepository.ListAsync(gym.Id);
    }
}