namespace GymApp.Contracts.Subscriptions;

public record CreateSubscriptionRequest(SubscriptionType SubscriptionType, Guid AdminId);