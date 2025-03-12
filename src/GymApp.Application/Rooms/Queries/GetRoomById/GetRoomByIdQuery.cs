using ErrorOr;
using GymApp.Domain.RoomAggregate;
using MediatR;

namespace GymApp.Application.Rooms.Queries.GetRoomById;

public record GetRoomByIdQuery(Guid GymId, Guid RoomId) : IRequest<ErrorOr<Room>>;