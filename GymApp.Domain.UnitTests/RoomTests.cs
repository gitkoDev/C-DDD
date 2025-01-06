using FluentAssertions;
using GymApp.Domain.Errors;
using GymApp.Domain.UnitTests.TestConstants;
using GymApp.Domain.UnitTests.TestUtils.Common;
using GymApp.Domain.UnitTests.TestUtils.Rooms;
using GymApp.Domain.UnitTests.TestUtils.Sessions;

namespace GymApp.Domain.UnitTests;

public class RoomTests
{
    [Fact]
    public void ScheduleSession_ForRoomWhoAlreadyMadeReservation_ShouldFail()
    {
        var room = RoomFactory.CreateRoom();
        var session = SessionFactory.CreateSession();

        var scheduleSessionFirstTimeResult = room.ScheduleSession(session);
        var scheduleSessionSecondTimeResult = room.ScheduleSession(session);

        scheduleSessionFirstTimeResult.IsError.Should().BeFalse();
        scheduleSessionSecondTimeResult.IsError.Should().BeTrue();
        scheduleSessionSecondTimeResult.FirstError.Should().Be(RoomErrors.SessionExists);
    }

    [Fact]
    public void ScheduleSession_WhenRoomLimitExceeded_ShouldFail()
    {
        var room = RoomFactory.CreateRoom();
        var session1 = SessionFactory.CreateSession(id: Guid.NewGuid());
        var session2 = SessionFactory.CreateSession(id: Guid.NewGuid());
        
        var scheduleSession1Result = room.ScheduleSession(session1);
        var scheduleSession2Result = room.ScheduleSession(session2);
    
        scheduleSession1Result.IsError.Should().BeFalse();
        scheduleSession2Result.IsError.Should().BeTrue();
        scheduleSession2Result.FirstError.Should().Be(RoomErrors.SessionsLimitExceeded);
    }
    
    [Theory]
    [InlineData(1, 3, 1, 3)]
    [InlineData(1, 3, 2, 3)]
    [InlineData(1, 3, 0, 2)]
    public void AddSessionToSchedule_WhenSessionOverlapsWithAnotherSessionInTheRoom_ShouldFail(
        int startHourSession1,
        int endHourSession1,
        int startHourSession2,
        int endHourSession2
    )
    {
        var room = RoomFactory.CreateRoom(maxDailySessions: 2);

        var session1 = SessionFactory.CreateSession(
            date: Constants.Session.Date,
            time: TimeRangeFactory.CreateFromHours(startHourSession1, endHourSession1),
            id: Guid.NewGuid());
        
        var session2 = SessionFactory.CreateSession(
            date: Constants.Session.Date,
            time: TimeRangeFactory.CreateFromHours(startHourSession2, endHourSession2),
            id: Guid.NewGuid());

        var scheduleSession1Result = room.ScheduleSession(session1);
        var scheduleSession2Result = room.ScheduleSession(session2);

        scheduleSession1Result.IsError.Should().BeFalse();
        scheduleSession2Result.IsError.Should().BeTrue();
        scheduleSession2Result.FirstError.Should().Be(RoomErrors.OverlappingSessions);
    }
}