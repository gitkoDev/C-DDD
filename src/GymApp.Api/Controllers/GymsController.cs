using ErrorOr;
using GymApp.Application.Gyms.Commands.AddTrainer;
using GymApp.Application.Gyms.Commands.CreateGym;
using GymApp.Application.Gyms.Queries.GetGymById;
using GymApp.Application.Gyms.Queries.ListGyms;
using GymApp.Contracts.Gyms;
using GymApp.Domain.GymAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GymApp.Api.Controllers;

[Route("subscriptions/{subscriptionId:guid}/gyms")]
public class GymsController : ApiController
{
    private readonly ISender _sender;

    public GymsController(ISender sender)
    {
        _sender = sender;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateGym(CreateGymRequest request, Guid subscriptionId)
    {
        var command = new CreateGymCommand(subscriptionId, request.Name);

        var createGymResult = await _sender.Send(command);

        return createGymResult.Match(
            gym => CreatedAtAction(
                nameof(GetGymById),
                new { subscriptionId, GymId = gym.Id },
                new GymResponse(gym.Id, request.Name)),
            Problem);
    }

    [HttpGet]
    public async Task<IActionResult> ListGyms(Guid subscriptionId)
    {
        var query = new ListGymsQuery(subscriptionId);
    
        var listGymsResult = await _sender.Send(query);

        return listGymsResult.Match(
            gyms => Ok(gyms.ConvertAll(gym => new GymResponse(gym.Id, gym.Name))),
            Problem);
    }

    [HttpGet("{gymId:guid}")]
    public async Task<IActionResult> GetGymById(Guid subscriptionId, Guid gymId)
    {
        var query = new GetGymByIdQuery(subscriptionId, gymId);

        var getGymByIdResult = await _sender.Send(query);

        return getGymByIdResult.Match(
            gym => Ok(new GymResponse(gymId, gym.Name)),
            Problem);

    }

    [HttpPost("{gymId:guid}/trainers")]
    public async Task<IActionResult> AddTrainer(AddTrainerRequest request, Guid gymId)
    {
        var command = new AddTrainerCommand(request.SubscriptionId, gymId, request.TrainerId);

        var result = await _sender.Send(command);
        
        return result.Match(
            trainer => Ok(new TrainerResponse()),
            Problem);
    }
}
