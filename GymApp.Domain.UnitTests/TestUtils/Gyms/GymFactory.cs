using GymApp.Domain.UnitTests.TestConstants;

namespace GymApp.Domain.UnitTests.TestUtils.Gyms;

public static class GymFactory
{
    public static Gym CreateGym(
        int maxRooms = Constants.Subscription.MaxRoomsFreeTier,
        Guid? id = null)
    {
        return new Gym(
            maxRooms: maxRooms,
            id: id ?? Guid.NewGuid()
            );
    }
}