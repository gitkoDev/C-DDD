using ErrorOr;

namespace GymApp.Domain.AdminAggregate;

public static class AdminErrors
{
    public static readonly Error SubscriptionActiveAlready = Error.Conflict(
        code: "Admin.SubscriptionActiveAlready",
        description: "This admin already has an active subscription");

    public static ErrorOr<Deleted> NoActiveSubscription = Error.Conflict(
        code: "Admin.NoActiveSubscription",
        description: "Admin's subscription is not active");

    public static Error WrongSubscription(Guid adminId)
    {
      return Error.Conflict(
            code: "Admin.WrongSubscription",
            description: $"Admin does not have subscription with id {adminId}");  
    }
    
    public static Error AdminNotFound(Guid adminId)
    {
        return Error.NotFound("Admin.AdminNotFound",
            description: $"Admin with id {adminId} not found");
    }

}