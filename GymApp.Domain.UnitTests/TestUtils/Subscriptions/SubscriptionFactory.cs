namespace GymApp.Domain.UnitTests.TestUtils.Subscriptions;

public static class SubscriptionFactory
{
    public static Subscription CreateSubscription(SubscriptionType? subscriptionType = SubscriptionType.Free, Guid? id = null)
    {
        return new Subscription(
            subscriptionType: subscriptionType ?? default,
            id: id ?? Guid.NewGuid()
            );
    }
}
