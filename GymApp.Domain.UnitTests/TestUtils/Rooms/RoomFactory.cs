using GymApp.Domain.RoomAggregate;
using GymApp.Domain.UnitTests.TestConstants;

namespace GymApp.Domain.UnitTests.TestUtils.Rooms;

public static class RoomFactory
{
    public static Room CreateRoom(
        int? maxDailySessions = Constants.Subscription.MaxRoomsFreeTier,  
        Guid? id = null)
    {
        return new Room(
            maxDailySessions: maxDailySessions ?? Constants.Subscription.MaxRoomsFreeTier,
            id: id ?? Constants.Room.Id);
    }
}