using GymApp.Domain.TrainerAggregate;

namespace GymApp.Application.Common.Interfaces;

public interface ITrainersRepository
{
    Task AddTrainerAsync(Trainer trainer);
    Task<Trainer?> GetByIdAsync(Guid id);
}