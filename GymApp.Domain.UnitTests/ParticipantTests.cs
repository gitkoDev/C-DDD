using FluentAssertions;
using GymApp.Domain.Errors;
using GymApp.Domain.UnitTests.TestUtils.Common;
using GymApp.Domain.UnitTests.TestUtils.Participants;
using GymApp.Domain.UnitTests.TestUtils.Services;
using GymApp.Domain.UnitTests.TestUtils.Sessions;
using Constants = GymApp.Domain.UnitTests.TestConstants.Constants;

namespace GymApp.Domain.UnitTests;

public class ParticipantTests
{
    [Fact]
    public void CancelReservation_ThatDoesNotExist_ShouldFail()
    {
        var participant = ParticipantFactory.CreateParticipant();

        var session = SessionFactory.CreateSession();
        var cancelNotExistingSessionResult = session.CancelReservation(participant, new TestDateTimeProvider());

        cancelNotExistingSessionResult.IsError.Should().BeTrue();
        cancelNotExistingSessionResult.FirstError.Should().Be(SessionErrors.ReservationNotFound);
    }
    
    [Theory]
    [InlineData(1, 3, 1, 3)]
    [InlineData(1, 3, 2, 3)]
    [InlineData(1, 3, 0, 2)]
    public void AddSessionToSchedule_WhenSessionOverlapsWithAnotherSession_ShouldFail(
        int startHourSession1,
        int endHourSession1,
        int startHourSession2,
        int endHourSession2
        )
    {
        var participant = ParticipantFactory.CreateParticipant();
    
        var session1 = SessionFactory.CreateSession(
            id: Guid.NewGuid(),
            date: Constants.Session.Date,
            time: TimeRangeFactory.CreateFromHours(startHourSession1, endHourSession1));
        
        var session2 = SessionFactory.CreateSession(
            id: Guid.NewGuid(),
            date: Constants.Session.Date,
            time: TimeRangeFactory.CreateFromHours(startHourSession2, endHourSession2));

        var reserveSpotSameTimeSession1Result = participant.AddToSchedule(session1);
        var reserveSpotSameTimeSession2Result = participant.AddToSchedule(session2);


        reserveSpotSameTimeSession1Result.IsError.Should().BeFalse();
        
        reserveSpotSameTimeSession2Result.IsError.Should().BeTrue();
        reserveSpotSameTimeSession2Result.FirstError.Should().Be(ParticipantErrors.OverlappingSessions);
    }

    [Fact]
    public void AddExistingSessionToSchedule_ShouldFail()
    {
        var participant = ParticipantFactory.CreateParticipant();
    
        var session1 = SessionFactory.CreateSession(
            id: Guid.NewGuid(),
            date: Constants.Session.Date,
            time: TimeRangeFactory.CreateFromHours(1, 2));

        var reserveSpotSameSessionResult1 = participant.AddToSchedule(session1);
        var reserveSpotSameSessionResult2 = participant.AddToSchedule(session1);
        
        reserveSpotSameSessionResult1.IsError.Should().BeFalse();
        
        reserveSpotSameSessionResult2.IsError.Should().BeTrue();
        reserveSpotSameSessionResult2.FirstError.Should().Be(ParticipantErrors.SessionExists);
    }
}