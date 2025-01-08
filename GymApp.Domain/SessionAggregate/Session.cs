using ErrorOr;
using GymApp.Domain.Common;
using GymApp.Domain.Common.ValueObjects;
using GymApp.Domain.ParticipantAggregate;

namespace GymApp.Domain.SessionAggregate;

public class Session(DateOnly date, TimeRange time, int maxParticipants, Guid trainerId, Guid? id = null) : AggregateRoot(id: id ?? Guid.NewGuid())
{
    public DateOnly Date { get; } = date;

    public TimeRange Time { get; } = time;


    private readonly Guid _trainerId = trainerId;
    private readonly List<Reservation> _reservations = [];

    public ErrorOr<Success> ReserveSpot(Participant participant)
    {
        if (_reservations.Count >= maxParticipants)
            return SessionErrors.ParticipantsLimitExceeded;
        
        _reservations.Add(new Reservation(participant.Id));

        return Result.Success;
    }

    public ErrorOr<Success> CancelReservation(Participant participant, IDateTimeProvider dateTimeProvider)
    {
        if (IsTooCloseToSession(dateTimeProvider.UtcNow))
            return SessionErrors.CancellationTooCloseToSessionTime;
        
        var reservation = _reservations.Find(r => r.ParticipantId == participant.Id);
        if (reservation is null)
            return SessionErrors.ReservationNotFound;
        
        if (!_reservations.Remove(reservation))
            return SessionErrors.ReservationNotFound;
    
        return Result.Success;
    }

    private bool IsTooCloseToSession(DateTime utcNow)
    {
        const int minHours = 24;
        return (Date.ToDateTime(Time.Start) - utcNow).TotalHours < minHours;
    }
}