using GymApp.Domain.AdminAggregate;

namespace GymApp.Application.Common.Interfaces;

public interface IAdminsRepository
{
    Task CreateAsync(Admin admin);
    Task<Admin?> GetByIdAsync(Guid adminId);
    Task UpdateAsync(Admin admin);
    Task DeleteAsync(Admin admin);
}