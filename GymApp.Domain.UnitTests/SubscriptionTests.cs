using FluentAssertions;
using GymApp.Domain.SubscriptionAggregate;
using GymApp.Domain.UnitTests.TestUtils.Gyms;
using GymApp.Domain.UnitTests.TestUtils.Subscriptions;

namespace GymApp.Domain.UnitTests;

public class SubscriptionTests
{
    [Fact]
    public void AddingGym_WhenLimitExceeded_ShouldFail()
    {
        var subscription = SubscriptionFactory.CreateSubscription(subscriptionType: SubscriptionType.Free);
        var gym1 = GymFactory.CreateGym();
        var gym2 = GymFactory.CreateGym();

        var addGym1Result = subscription.AddGym(gym1);
        var addGym2Result = subscription.AddGym(gym2);

        addGym1Result.IsError.Should().BeFalse();
        addGym2Result.IsError.Should().BeTrue();
        addGym2Result.FirstError.Should().Be(SubscriptionErrors.GymLimitExceeded);
    }
}