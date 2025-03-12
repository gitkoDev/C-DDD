using ErrorOr;
using GymApp.Domain.SubscriptionAggregate;
using MediatR;

namespace GymApp.Application.Subscriptions.Commands.CreateSubscription;

public record CreateSubscriptionCommand(SubscriptionType SubscriptionType, Guid AdminId) : IRequest<ErrorOr<Subscription>>;