using GymApp.Domain.Common.ValueObjects;
using GymApp.Domain.SubscriptionAggregate;

namespace GymApp.Domain.AdminAggregate.Events;

public record SubscriptionSetEvent(Admin Admin, Subscription Subscription) : IDomainEvent;