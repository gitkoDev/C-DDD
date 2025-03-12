using ErrorOr;
using GymApp.Application.Common.Interfaces;
using GymApp.Domain.AdminAggregate;
using MediatR;

namespace GymApp.Application.Admins.Commands.CreateAdmin;

public class CreateAdminCommandHandler : IRequestHandler<CreateAdminCommand, ErrorOr<Admin>>
{
    private readonly IAdminsRepository _adminsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAdminCommandHandler(
        IAdminsRepository adminsRepository,
        IUnitOfWork unitOfWork)
    {
        _adminsRepository = adminsRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<ErrorOr<Admin>> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
    {
        var admin = new Admin(request.UserId, null);
        
        await _adminsRepository.CreateAsync(admin);
        await _unitOfWork.CommitChangesAsync();
        return admin;
    }
}