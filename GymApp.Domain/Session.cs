using ErrorOr;
using GymApp.Domain.Errors;

namespace GymApp.Domain;

public class Session
{
    public Guid Id { get; }
    
    public DateOnly Date { get; }
    
    public TimeRange Time { get; }

    
    private readonly Guid _trainerId;
    private readonly List<Guid> _participantIds = [];
    
    private readonly int _maxParticipants;

    public Session(DateOnly date, TimeRange time, int maxParticipants, Guid trainerId, Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        _maxParticipants = maxParticipants;
        _trainerId = trainerId;
        Date = date;
        Time = time;
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
        if (!_participantIds.Contains(participant.Id))
            return SessionErrors.ReservationNotFound;
        
        if (IsTooCloseToSession(dateTimeProvider.UtcNow))
            return SessionErrors.CancellationTooCloseToSessionTime;
    
        if (!_participantIds.Remove(participant.Id))
            return SessionErrors.ReservationNotFound;
    
        return Result.Success;
    }

    private bool IsTooCloseToSession(DateTime utcNow)
    {
        const int minHours = 24;
        return (Date.ToDateTime(Time.Start) - utcNow).TotalHours < minHours;
    }
}