using ErrorOr;
using GymApp.Application.Common.Interfaces;
using GymApp.Domain.SubscriptionAggregate;
using MediatR;

namespace GymApp.Application.Subscriptions.Commands.DeleteSubscription;

public class DeleteSubscriptionCommandHandler : IRequestHandler<DeleteSubscriptionCommand, ErrorOr<Deleted>>
{
    private readonly IAdminsRepository _adminsRepository;
    private readonly ISubscriptionsRepository _subscriptionsRepository;
    private readonly IGymsRepository _gymsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSubscriptionCommandHandler(
        IAdminsRepository adminsRepository,
        ISubscriptionsRepository subscriptionsRepository,
        IGymsRepository gymsRepository,
        IUnitOfWork unitOfWork)
    {
        _adminsRepository = adminsRepository;
        _subscriptionsRepository = subscriptionsRepository;
        _gymsRepository = gymsRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<ErrorOr<Deleted>> Handle(DeleteSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionsRepository.GetByIdAsync(request.SubscriptionId);
        if (subscription is null)
            return SubscriptionErrors.SubscriptionNotFound(request.SubscriptionId);

        var admin = await _adminsRepository.GetByIdAsync(subscription.AdminId);
        if (admin is null)
            return SubscriptionErrors.AdminNotFound(subscription.AdminId);

        admin.DeleteSubscription(subscription);

        var gymsToDelete = await _gymsRepository.ListAsync(subscription.Id);

        await _adminsRepository.UpdateAsync(admin);
        await _subscriptionsRepository.RemoveAsync(subscription);
        await _gymsRepository.RemoveRangeAsync(gymsToDelete);
        await _unitOfWork.CommitChangesAsync();
        
        return Result.Deleted;
    }
}