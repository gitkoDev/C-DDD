namespace GymApp.Domain.UnitTests.TestConstants;

public static partial class Constants
{
    public static class Session
    {
        public static readonly Guid Id = Guid.NewGuid();

        public const int MaxParticipants = 1;

        public static readonly DateOnly Date = new DateOnly();

        public static readonly TimeOnly StartTime = new TimeOnly();
        
        public static readonly TimeOnly EndTime = new TimeOnly();
    }
}