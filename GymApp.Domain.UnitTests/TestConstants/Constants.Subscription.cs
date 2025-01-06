namespace GymApp.Domain.UnitTests.TestConstants;

public static partial class Constants
{
    public static class Subscription
    {
        public static readonly Guid Id = Guid.NewGuid();
        
        public const int MaxRoomsFreeTier = 1;
        public const int MaxGymsFreeTier = 1;
        public const int MaxDailySessionsFreeTier = 4;
    }
}