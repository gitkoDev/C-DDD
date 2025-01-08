using ErrorOr;
using GymApp.Domain.Common;
using GymApp.Domain.GymAggregate;

namespace GymApp.Domain.SubscriptionAggregate;

public class Subscription : AggregateRoot
{
    private readonly List<Guid> _gymIds = []; 
    private readonly SubscriptionType _subscriptionType;
    
    private readonly int _maxGyms;
    
    public Subscription(SubscriptionType subscriptionType, Guid? id = null) : base(id: id ?? Guid.NewGuid())
    {
        _subscriptionType = subscriptionType;
        _maxGyms = GetMaxGyms();
    }

    public ErrorOr<Success> AddGym(Gym gym)
    {
        if (_gymIds.Count >= _maxGyms)
            return SubscriptionErrors.GymLimitExceeded;
            
        _gymIds.Add(gym.Id);

        return Result.Success;
    }
    
    private int GetMaxGyms()
    {
        return _subscriptionType switch
        {
            (SubscriptionType.Free) => 1,
            (SubscriptionType.Starter) => 1,
            (SubscriptionType.Pro) => 3,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}

public enum SubscriptionType
{
    Free = 1,
    Starter = 2,
    Pro = 3,
}