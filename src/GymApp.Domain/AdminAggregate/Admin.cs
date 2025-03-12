using ErrorOr;
using GymApp.Domain.AdminAggregate.Events;
using GymApp.Domain.Common;
using GymApp.Domain.SubscriptionAggregate;

namespace GymApp.Domain.AdminAggregate;

public class Admin : AggregateRoot
{
    //EF
    private Admin(){}
    
    public Admin(
        Guid userId, Guid? subscriptionId, Guid? id = null) : 
        base(id ?? Guid.NewGuid())
    {
        UserId = userId;
        SubscriptionId = subscriptionId;
    }
   
    public Guid UserId { get; }
    public Guid? SubscriptionId { get; private set; }

    public ErrorOr<Success> SetSubscription(Subscription subscription)
    {
        if (SubscriptionId.HasValue)
            return AdminErrors.SubscriptionActiveAlready;

        SubscriptionId = subscription.Id;
        
        _domainEvents.Add(new SubscriptionSetEvent(this, subscription));

        return Result.Success;
    }

    public ErrorOr<Deleted> DeleteSubscription(Subscription subscription)
    {
        if (!SubscriptionId.HasValue)
            return AdminErrors.NoActiveSubscription;

        if (SubscriptionId != subscription.Id)
            return AdminErrors.WrongSubscription(subscription.Id);

        SubscriptionId = null;
        
        return Result.Deleted;
    }
}