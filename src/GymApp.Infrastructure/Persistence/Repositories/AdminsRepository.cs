using GymApp.Application.Common.Interfaces;
using GymApp.Domain.AdminAggregate;
using GymApp.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace GymApp.Infrastructure.Persistence.Repositories;

public class AdminsRepository : IAdminsRepository
{
    private readonly GymManagementDbContext _dbContext;

    public AdminsRepository(GymManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task CreateAsync(Admin admin)
    {
        await _dbContext.Admins.AddAsync(admin);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Admin?> GetByIdAsync(Guid adminId)
    {
        return await _dbContext.Admins.FindAsync(adminId);
    }

    public async Task DeleteAsync(Admin admin)
    {
        _dbContext.Admins.Remove(admin);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Admin admin)
    {
        _dbContext.Update(admin);
        await _dbContext.SaveChangesAsync();
    }
}