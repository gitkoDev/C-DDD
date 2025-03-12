using GymApp.Application.Subscriptions.Commands.CreateSubscription;
using GymApp.Application.Subscriptions.Commands.DeleteSubscription;
using GymApp.Application.Subscriptions.Queries;
using GymApp.Application.Subscriptions.Queries.GetSubscriptionById;
using GymApp.Application.Subscriptions.Queries.ListSubscriptions;
using GymApp.Contracts.Subscriptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DomainSubscriptionType = GymApp.Domain.SubscriptionAggregate.SubscriptionType;

namespace GymApp.Api.Controllers;

[Route("subscriptions")]
public class SubscriptionsController : ApiController
{
    private readonly ISender _sender;

    public SubscriptionsController(ISender sender)
    {
        _sender = sender;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateSubscription(CreateSubscriptionRequest request)
    {
        if (!DomainSubscriptionType.TryFromName(request.SubscriptionType.ToString(), out var subscriptionType))
        {
            Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Invalid Subscription Type");
        }
        
        var command = new CreateSubscriptionCommand(subscriptionType, request.AdminId);
        var createSubscriptionResult = await _sender.Send(command);

        return createSubscriptionResult.Match(
            subscription => Ok(new SubscriptionResponse(subscription.Id, request.SubscriptionType)),
            Problem);
    }

    [HttpGet("{subscriptionId:guid}")]
    public async Task<IActionResult> GetSubscriptionById(Guid subscriptionId)
    {
        var query = new GetSubscriptionByIdQuery(subscriptionId);

        var getSubscriptionByIdResult = await _sender.Send(query);

        return getSubscriptionByIdResult.Match(
            subscription =>
                Ok(new SubscriptionResponse(subscription.Id, Enum.Parse<SubscriptionType>(subscription.SubscriptionType.Name))),
            Problem);
    }

    [HttpGet]
    public async Task<IActionResult> ListSubscriptions()
    {
        var query = new ListSubscriptionsQuery();

        var listSubscriptionsResult = await _sender.Send(query);

        return listSubscriptionsResult.Match(
            subscriptions => Ok(subscriptions.ConvertAll(subscription =>
                new SubscriptionResponse(subscription.Id, MatchSubscriptionType(subscription.SubscriptionType)))),
        Problem);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteSubscription(Guid subscriptionId)
    {
        var command = new DeleteSubscriptionCommand(subscriptionId);

        var deleteSubscriptionResult = await _sender.Send(command);

        return deleteSubscriptionResult.Match<IActionResult>(
            _ => NoContent(),
            Problem);
    }

    private static SubscriptionType MatchSubscriptionType(DomainSubscriptionType subscriptionType)
    {
        return subscriptionType.Name switch
        {
            nameof(DomainSubscriptionType.Free) => SubscriptionType.Free,
            nameof(DomainSubscriptionType.Starter) => SubscriptionType.Starter,
            nameof(DomainSubscriptionType.Pro) => SubscriptionType.Pro,
            _ => throw new InvalidCastException()
        };
    }
}