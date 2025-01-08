using ErrorOr;

namespace GymApp.Domain.SubscriptionAggregate;

public static class SubscriptionErrors
{
    public static readonly Error GymLimitExceeded = Error.Validation(
        code: "Subscription.GymsLimitExceeded",
        description: "No more gyms can be added, limit exceeded");
}