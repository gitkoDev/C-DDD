using GymApp.Application.Rooms.Commands.CreateRoom;
using GymApp.Application.Rooms.Queries.GetRoomById;
using GymApp.Application.Rooms.Queries.ListRooms;
using GymApp.Contracts.Rooms;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymApp.Api.Controllers;

[Route("gyms/{gymId:guid}/rooms")]
public class RoomsController : ApiController
{
    private readonly ISender _sender;

    public RoomsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoom(CreateRoomRequest request, Guid gymId)
    {
        var command = new CreateRoomCommand(request.RoomName, gymId);

        var result = await _sender.Send(command);

        return result.Match(
            room => CreatedAtAction(
                nameof(GetRoomById), 
                new { gymId, room.Id },
                new RoomResponse(room.Id, room.Name)), 
            Problem);
    }

    [HttpGet]
    public async Task<IActionResult> ListRooms(Guid gymId)
    {
        var query = new ListRoomsQuery(gymId);

        var result = await _sender.Send(query);
        
        return result.Match(
            rooms => Ok(rooms.ConvertAll(room => new RoomResponse(room.Id, room.Name))),
            Problem);
    }
    
    [HttpGet("{roomId:guid}")]
    public async Task<IActionResult> GetRoomById(Guid gymId, Guid roomId)
    {
        var query = new GetRoomByIdQuery(gymId, roomId);

        var result = await _sender.Send(query);

        return result.Match(
            room => Ok(new RoomResponse(room.Id, room.Name)),
            Problem);
    }
}