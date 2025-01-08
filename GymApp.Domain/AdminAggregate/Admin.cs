using GymApp.Domain.Common;

namespace GymApp.Domain.AdminAggregate;

public class Admin(Guid userId, Guid subscriptionId, Guid? id = null) : AggregateRoot(id: id ?? Guid.NewGuid())
{
    private readonly Guid _userId = userId;
    private readonly Guid _subscriptionId = subscriptionId;
}