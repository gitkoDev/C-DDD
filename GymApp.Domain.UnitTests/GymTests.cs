using FluentAssertions;
using GymApp.Domain.GymAggregate;
using GymApp.Domain.UnitTests.TestUtils.Gyms;
using GymApp.Domain.UnitTests.TestUtils.Rooms;

namespace GymApp.Domain.UnitTests;

public class GymTests
{
    [Fact]
    public void AddingRoom_WhenLimitExceeded_ShouldFail()
    {
        var gym = GymFactory.CreateGym();
        var room1 = RoomFactory.CreateRoom(id: Guid.NewGuid());
        var room2 = RoomFactory.CreateRoom(id: Guid.NewGuid());

        var gymAddRoom1Result = gym.AddRoom(room1);
        var gymAddRoom2Result = gym.AddRoom(room2);

        gymAddRoom1Result.IsError.Should().BeFalse();
        gymAddRoom2Result.IsError.Should().BeTrue();
        gymAddRoom2Result.FirstError.Should().Be(GymErrors.RoomLimitExceeded);
    }

    [Fact]
    public void AddingExistingRoom_ShouldFail()
    {
        var gym = GymFactory.CreateGym();
        var room = RoomFactory.CreateRoom();

        var gymAddRoomFirstTimeResult = gym.AddRoom(room);
        var gymAddRoomSecondTimeResult = gym.AddRoom(room);

        gymAddRoomFirstTimeResult.IsError.Should().BeFalse();
        gymAddRoomSecondTimeResult.IsError.Should().BeTrue();
        gymAddRoomSecondTimeResult.FirstError.Should().Be(GymErrors.RoomExists);
    }
}