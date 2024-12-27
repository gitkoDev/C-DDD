namespace GymApp.Domain;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
}