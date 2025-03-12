using ErrorOr;
using GymApp.Application.Common.Interfaces;
using GymApp.Domain.AdminAggregate;
using GymApp.Domain.SubscriptionAggregate;
using MediatR;

namespace GymApp.Application.Subscriptions.Commands.CreateSubscription;

public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, ErrorOr<Subscription>>
{
    private readonly IAdminsRepository _adminsRepository;
    private readonly ISubscriptionsRepository _subscriptionsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSubscriptionCommandHandler(
        IAdminsRepository adminsRepository,
        ISubscriptionsRepository subscriptionsRepository, 
        IUnitOfWork unitOfWork)
    {
        _adminsRepository = adminsRepository;
        _subscriptionsRepository = subscriptionsRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<ErrorOr<Subscription>> Handle(CreateSubscriptionCommand command, CancellationToken cancellationToken)
    {
        var admin = await _adminsRepository.GetByIdAsync(command.AdminId);
        if(admin is null)
            return Error.NotFound($"Admin with id {command.AdminId} not found");

        if (admin.SubscriptionId is not null)
            return AdminErrors.SubscriptionActiveAlready;
        
        var subscription = new Subscription(command.SubscriptionType, command.AdminId);
        admin.SetSubscription(subscription);

        await _adminsRepository.UpdateAsync(admin);

        return subscription;
    }
}