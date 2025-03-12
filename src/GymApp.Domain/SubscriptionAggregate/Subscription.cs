using Ardalis.SmartEnum;
using ErrorOr;
using GymApp.Domain.Common;
using GymApp.Domain.GymAggregate;

namespace GymApp.Domain.SubscriptionAggregate;

public class Subscription
{
    private readonly List<Guid> _gymIds = []; 
    private readonly int _maxGyms;

    public SubscriptionType SubscriptionType { get; private set; }

    public Guid AdminId { get; }

    public Guid Id { get; private set; }
    
    public Subscription(SubscriptionType subscriptionType, Guid adminId, Guid? id = null)
    {
        SubscriptionType = subscriptionType;
        AdminId = adminId;
        Id = id ?? Guid.NewGuid();
        _maxGyms = GetMaxGyms();
    }

    public ErrorOr<Success> AddGym(Gym gym)
    {
        if (_gymIds.Contains(gym.Id))
            return SubscriptionErrors.GymInSubscriptionAlreadyExists(gym.Id);
        
        if (_gymIds.Count >= _maxGyms)
            return SubscriptionErrors.GymLimitExceeded;
            
        _gymIds.Add(gym.Id);

        return Result.Success;
    }

    public int GetMaxRooms() => SubscriptionType.Name switch
    {
        nameof(SubscriptionType.Free) => 1,
        nameof(SubscriptionType.Starter) => 3,
        nameof(SubscriptionType.Pro) => int.MaxValue,
        _ => throw new ArgumentException("Unknown subscription type")
    };
    
    public int GetMaxGyms()
    {
        return SubscriptionType.Name switch
        {
            nameof(SubscriptionType.Free) => 1,
            nameof(SubscriptionType.Starter) => 1,
            nameof(SubscriptionType.Pro) => 3,
            _ => throw new ArgumentException("Unknown subscription type")
        };
    }

    public int GetMaxDailySessions()
    {
        return SubscriptionType.Name switch
        {
            nameof(SubscriptionType.Free) => 4,
            nameof(SubscriptionType.Starter) => int.MaxValue,
            nameof(SubscriptionType.Pro) => int.MaxValue,
            _ => throw new ArgumentException("Unknown subscription type")
        };
    }
    
    private Subscription() {}
}

public class SubscriptionType : SmartEnum<SubscriptionType>
{
    public static readonly SubscriptionType Free = new(nameof(Free), 0);
    public static readonly SubscriptionType Starter = new(nameof(Starter), 1);
    public static readonly SubscriptionType Pro = new(nameof(Pro), 2);

    
    public SubscriptionType(string name, int value) : base(name, value) {}
}