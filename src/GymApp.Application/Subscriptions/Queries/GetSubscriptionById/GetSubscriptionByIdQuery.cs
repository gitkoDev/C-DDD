using ErrorOr;
using GymApp.Domain.SubscriptionAggregate;
using MediatR;

namespace GymApp.Application.Subscriptions.Queries.GetSubscriptionById;

public record GetSubscriptionByIdQuery(Guid SubscriptionId) : IRequest<ErrorOr<Subscription>>;