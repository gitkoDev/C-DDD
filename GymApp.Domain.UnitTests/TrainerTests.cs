using FluentAssertions;
using GymApp.Domain.Errors;
using GymApp.Domain.UnitTests.TestUtils.Common;
using GymApp.Domain.UnitTests.TestUtils.Sessions;
using GymApp.Domain.UnitTests.TestUtils.Trainers;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Constants = GymApp.Domain.UnitTests.TestConstants.Constants;

namespace GymApp.Domain.UnitTests;

public class TrainerTests
{
    [Fact]
    public void AddExistingSessionToSchedule_ShouldFail()
    {
        var trainer = TrainerFactory.CreateTrainer();

        var session = SessionFactory.CreateSession();

        var addSessionResult1 = trainer.AddSessionToSchedule(session);
        var addSessionResult2 = trainer.AddSessionToSchedule(session);

        addSessionResult1.IsError.Should().BeFalse();
        addSessionResult2.IsError.Should().BeTrue();
        addSessionResult2.FirstError.Should().Be(TrainerErrors.SessionExists);
    }
    
    [Theory]
    [InlineData(1, 3, 1, 3)]
    [InlineData(1, 3, 2, 3)]
    [InlineData(1, 3, 0, 2)]
    public void AddSessionToSchedule_WhenSessionOverlapsWithAnotherSessionForTrainer_ShouldFail(
        int startHourSession1,
        int endHourSession1,
        int startHourSession2,
        int endHourSession2
    )
    {
        var trainer = TrainerFactory.CreateTrainer();

        var session1 = SessionFactory.CreateSession(
            id: Guid.NewGuid(),
            date: Constants.Session.Date,
            time: TimeRangeFactory.CreateFromHours(startHourSession1, endHourSession1));
        
        var session2 = SessionFactory.CreateSession(
            id: Guid.NewGuid(),
            date: Constants.Session.Date,
            time: TimeRangeFactory.CreateFromHours(startHourSession2, endHourSession2));

        var addSession1Result = trainer.AddSessionToSchedule(session1);
        var addSession2Result = trainer.AddSessionToSchedule(session2);

        addSession1Result.IsError.Should().BeFalse();
        addSession2Result.IsError.Should().BeTrue();
        addSession2Result.FirstError.Should().Be(TrainerErrors.OverlappingSessions);
    }
}