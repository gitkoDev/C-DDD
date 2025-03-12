using ErrorOr;
using GymApp.Domain.RoomAggregate;
using MediatR;

namespace GymApp.Application.Rooms.Queries.ListRooms;

public record ListRoomsQuery(Guid GymId) : IRequest<ErrorOr<List<Room>>>;