using GymApp.Application.Common.Interfaces;
using GymApp.Domain.TrainerAggregate;
using GymApp.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace GymApp.Infrastructure.Persistence.Repositories;

public class TrainersRepository : ITrainersRepository
{
    private readonly GymManagementDbContext _dbContext;

    public TrainersRepository(GymManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddTrainerAsync(Trainer trainer)
    {
       await _dbContext.Trainers.AddAsync(trainer);
    }
    
    public async Task<Trainer?> GetByIdAsync(Guid trainerId)
    {
        return await _dbContext.Trainers.FindAsync(trainerId);
    }
}