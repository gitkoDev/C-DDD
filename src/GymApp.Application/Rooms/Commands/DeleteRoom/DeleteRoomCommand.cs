using ErrorOr;
using GymApp.Domain.RoomAggregate;
using MediatR;

namespace GymApp.Application.Rooms.Commands.DeleteRoom;

public record DeleteRoomCommand(Guid GymId, Guid RoomId) : IRequest<ErrorOr<Deleted>>;