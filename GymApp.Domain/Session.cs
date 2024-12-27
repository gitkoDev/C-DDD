using ErrorOr;

namespace GymApp.Domain;

public class Session
{
    private readonly Guid _sessionId;
    private readonly Guid _trainerId;
    private readonly List<Guid> _participantIds = [];
    private readonly DateOnly _date;
    private readonly TimeOnly _startTime;
    private readonly TimeOnly _endTime;

    private readonly int _maxParticipants;

    public Session(DateOnly date, TimeOnly startTime, TimeOnly endTime, int maxParticipants, Guid trainerId, Guid? id = null)
    {
        _sessionId = id ?? Guid.NewGuid();
        _maxParticipants = maxParticipants;
        _trainerId = trainerId;
        _date = date;
        _startTime = startTime;
        _endTime = endTime;
    }

    public ErrorOr<Success> ReserveSpot(Participant participant)
    {
        if (_participantIds.Count >= _maxParticipants)
            return SessionErrors.ParticipantsLimitExceeded;
        
        _participantIds.Add(participant.Id);

        return Result.Success;
    }

    public ErrorOr<Success> CancelReservation(Participant participant, IDateTimeProvider dateTimeProvider)
    {
        if (IsTooCloseToSession(dateTimeProvider.UtcNow))
            return SessionErrors.CancellationTooCloseToSessionTime;

        if (!_participantIds.Remove(participant.Id))
            return SessionErrors.ReservationNotFound;

        return Result.Success;
    }

    private bool IsTooCloseToSession(DateTime utcNow)
    {
        const int minHours = 24;
        return (_date.ToDateTime(_startTime) - utcNow).TotalHours < minHours;
    }
}