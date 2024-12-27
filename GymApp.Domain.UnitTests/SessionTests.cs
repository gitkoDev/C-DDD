using GymApp.Domain.UnitTests.Sessions;
using FluentAssertions;
using GymApp.Domain.UnitTests.TestConstants;
using GymApp.Domain.UnitTests.TestUtils.Participants;
using GymApp.Domain.UnitTests.TestUtils.Services;

namespace GymApp.Domain.UnitTests;

public class SessionTests
{
    [Fact]
    public void ReserveSpot_WhenNoMoreRoom_ShouldFailReservation()
    {
        var session = SessionFactory.CreateSession(            
            date: Constants.Session.Date,
            startTime: Constants.Session.StartTime,
            endTime: Constants.Session.EndTime);
        var participant1 = ParticipantFactory.CreateParticipant(id: Guid.NewGuid(), userId: Guid.NewGuid());
        var participant2 = ParticipantFactory.CreateParticipant(id: Guid.NewGuid(), userId: Guid.NewGuid());

        var reserveParticipant1Result = session.ReserveSpot(participant1);
        var reserveParticipant2Result = session.ReserveSpot(participant2);

        reserveParticipant1Result.IsError.Should().BeFalse();
        reserveParticipant2Result.IsError.Should().BeTrue();
        reserveParticipant2Result.FirstError.Should().Be(SessionErrors.ParticipantsLimitExceeded);
    }

    [Fact]
    public void CancelReservation_WhenTooCloseToSession_ShouldFail()
    {
        var session = SessionFactory.CreateSession(
            date: Constants.Session.Date,
            startTime: Constants.Session.StartTime,
            endTime: Constants.Session.EndTime);
        var participant = ParticipantFactory.CreateParticipant();
        var cancellationDateTime = Constants.Session.Date.ToDateTime(TimeOnly.MinValue);
        
        var reserveParticipantResult = session.ReserveSpot(participant);
        var cancelReservationParticipantResult = session.CancelReservation(participant, new TestDateTimeProvider(cancellationDateTime));

        reserveParticipantResult.IsError.Should().BeFalse();
        cancelReservationParticipantResult.IsError.Should().BeTrue();
        cancelReservationParticipantResult.FirstError.Should().Be(SessionErrors.CancellationTooCloseToSessionTime);
    }

    [Fact]
    public void CancelReservation_ForParticipantWhoDidNotMakeReservation_ShouldFail()
    {
        var session = SessionFactory.CreateSession(
            date: Constants.Session.Date.AddDays(2),
            startTime: Constants.Session.StartTime,
            endTime: Constants.Session.EndTime);
        var participant = ParticipantFactory.CreateParticipant();
        var cancellationDateTime = Constants.Session.Date.ToDateTime(TimeOnly.MinValue);
        
        var cancelReservationParticipantResult = session.CancelReservation(participant, new TestDateTimeProvider(cancellationDateTime));

        cancelReservationParticipantResult.IsError.Should().BeTrue();
        cancelReservationParticipantResult.FirstError.Should().Be(SessionErrors.ReservationNotFound);
    }
}