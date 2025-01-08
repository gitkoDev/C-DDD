namespace GymApp.Domain.Common;

public abstract class Entity(Guid id)
{
    public Guid Id { get; } = id;

    public override bool Equals(object? obj)
    {
        if (obj is null || GetType() != obj.GetType())
            return false;

        return ((Entity)obj).Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}