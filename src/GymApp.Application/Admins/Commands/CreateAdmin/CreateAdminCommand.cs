using ErrorOr;
using GymApp.Domain.AdminAggregate;
using MediatR;

namespace GymApp.Application.Admins.Commands.CreateAdmin;

public record CreateAdminCommand(Guid UserId, Admin Admin) : IRequest<ErrorOr<Admin>>;