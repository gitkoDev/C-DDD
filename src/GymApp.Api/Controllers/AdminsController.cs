using GymApp.Application.Admins.Commands.CreateAdmin;
using GymApp.Application.Admins.Queries.GetAdminById;
using GymApp.Contracts.Admins;
using GymApp.Domain.AdminAggregate;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GymApp.Api.Controllers;

[Route("admins")]
public class AdminsController : ApiController
{
    private readonly ISender _sender;

    public AdminsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAdmin()
    {
        var command = new CreateAdminCommand(Guid.NewGuid(), new Admin(userId: Guid.NewGuid(), null));

        var result = await _sender.Send(command);

        return result.Match(
            admin => CreatedAtAction(nameof(GetAdminById), 
                new { AdminId = admin.Id },
                new AdminResponse(admin.Id, admin.SubscriptionId)),
            Problem);
    }

    [HttpGet("{adminId:guid}")]
    public async Task<IActionResult> GetAdminById(Guid adminId)
    {
        var query = new GetAdminByIdQuery(adminId);

        var result = await _sender.Send(query);

        return result.Match(
            admin => Ok(new AdminResponse(admin.Id, admin.SubscriptionId)),
            Problem);
    }
    
    

}