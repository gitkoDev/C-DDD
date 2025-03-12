using ErrorOr;
using GymApp.Domain.SubscriptionAggregate;
using MediatR;

namespace GymApp.Application.Subscriptions.Queries.ListSubscriptions;

public record ListSubscriptionsQuery : IRequest<ErrorOr<List<Subscription>>>;
