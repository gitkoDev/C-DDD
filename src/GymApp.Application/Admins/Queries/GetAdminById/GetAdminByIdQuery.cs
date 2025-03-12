using ErrorOr;
using GymApp.Domain.AdminAggregate;
using MediatR;

namespace GymApp.Application.Admins.Queries.GetAdminById;

public record GetAdminByIdQuery(Guid AdminId) : IRequest<ErrorOr<Admin>>;