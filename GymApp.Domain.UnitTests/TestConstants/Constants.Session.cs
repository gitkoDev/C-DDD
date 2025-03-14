using GymApp.Domain.Common.ValueObjects;
using GymApp.Domain.SessionAggregate;

namespace GymApp.Domain.UnitTests.TestConstants;

public static partial class Constants
{
    public static class Session
    {
        public static readonly Guid Id = Guid.NewGuid();

        public const int MaxParticipants = 1;

        public static readonly DateOnly Date = new();
        public static readonly TimeRange Time = new(
            TimeOnly.MinValue.AddHours(8),
            TimeOnly.MinValue.AddHours(9));
    }
}