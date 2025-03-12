using ErrorOr;
using GymApp.Application.Common.Interfaces;
using GymApp.Domain.AdminAggregate;
using MediatR;

namespace GymApp.Application.Admins.Queries.GetAdminById;

public class GetAdminByIdQueryHandler : IRequestHandler<GetAdminByIdQuery, ErrorOr<Admin>>
{
    private readonly IAdminsRepository _adminsRepository;

    public GetAdminByIdQueryHandler(IAdminsRepository adminsRepository)
    {
        _adminsRepository = adminsRepository;
    }
    
    public async Task<ErrorOr<Admin>> Handle(GetAdminByIdQuery request, CancellationToken cancellationToken)
    {
        var admin = await _adminsRepository.GetByIdAsync(request.AdminId);

        return admin is null 
            ? AdminErrors.AdminNotFound(request.AdminId) 
            : admin;
    }
}