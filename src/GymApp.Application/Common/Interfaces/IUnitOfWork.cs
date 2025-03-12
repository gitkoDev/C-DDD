namespace GymApp.Application.Common.Interfaces;

public interface IUnitOfWork
{
    Task CommitChangesAsync();
}