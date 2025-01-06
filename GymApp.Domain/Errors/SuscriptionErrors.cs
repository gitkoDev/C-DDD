using ErrorOr;

namespace GymApp.Domain.Errors;

public class SubscriptionErrors
{
    public static readonly Error GymLimitExceeded = Error.Validation(
        code: "Subscription.GymsLimitExceeded",
        description: "No more gyms can be added, limit exceeded");
}