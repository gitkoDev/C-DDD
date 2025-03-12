using GymApp.Domain.GymAggregate;
using GymApp.Domain.TrainerAggregate;

namespace GymApp.Application.Common.Interfaces;

public interface IGymsRepository
{
    Task CreateAsync(Gym gym);
    Task<Gym?> GetByIdAsync(Guid gymId);
    Task<List<Gym>> ListAsync(Guid subscriptionId);
    Task RemoveAsync(Gym gym);
    Task RemoveRangeAsync(List<Gym> gyms);
    Task UpdateAsync(Gym gym);
}