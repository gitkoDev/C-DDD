using ErrorOr;
using GymApp.Domain.RoomAggregate;
using MediatR;

namespace GymApp.Application.Rooms.Commands.CreateRoom;

public record CreateRoomCommand(string RoomName, Guid GymId) : IRequest<ErrorOr<Room>>;