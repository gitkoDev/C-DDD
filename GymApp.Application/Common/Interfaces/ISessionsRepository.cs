using GymApp.Domain.SessionAggregate;

namespace GymApp.Application.Common.Interfaces;

public interface ISessionsRepository
{
    Task AddSessionAsync(Session session);

    Task UpdateSessionAsync(Session session);
}