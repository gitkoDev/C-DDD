using ErrorOr;

namespace GymApp.Domain.SubscriptionAggregate;

public static class SubscriptionErrors
{
    public static readonly Error GymLimitExceeded = Error.Validation(
        code: "Subscription.GymLimitExceeded",
        description: "No more gyms can be added, limit exceeded");
    
    public static Error SubscriptionNotFound(Guid subscriptionId)
    {
        return Error.NotFound("Subscription.SubscriptionNotFound",
            description: $"Subscription with id {subscriptionId} not found");
    }
    
    public static Error GymInSubscriptionAlreadyExists(Guid subscriptionId)
    {
        return Error.Validation("Subscription.GymInSubscriptionAlreadyExists",
            description: $"Gym with id {subscriptionId} not found");
    }

    public static Error AdminNotFound(Guid adminId)
    {
        return Error.Unexpected("Subscription.AdminNotFound",
            description: $"Admin with id {adminId} not found");
    }
}