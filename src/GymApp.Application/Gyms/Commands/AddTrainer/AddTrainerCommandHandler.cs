using ErrorOr;
using GymApp.Application.Common.Interfaces;
using GymApp.Domain.AdminAggregate;
using GymApp.Domain.TrainerAggregate;
using MediatR;

namespace GymApp.Application.Gyms.Commands.AddTrainer;

public class AddTrainerCommandHandler : IRequestHandler<AddTrainerCommand, ErrorOr<Trainer>>
{
    private readonly ISubscriptionsRepository _subscriptionsRepository;
    private readonly IGymsRepository _gymsRepository;
    private readonly ITrainersRepository _trainersRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddTrainerCommandHandler(
        ISubscriptionsRepository subscriptionsRepository,
        IGymsRepository gymsRepository,
        ITrainersRepository trainersRepository,
        IUnitOfWork unitOfWork)
    {
        _subscriptionsRepository = subscriptionsRepository;
        _gymsRepository = gymsRepository;
        _trainersRepository = trainersRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<ErrorOr<Trainer>> Handle(AddTrainerCommand request, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionsRepository.GetByIdAsync(request.SubscriptionId);
        if (subscription is null)
            return Error.NotFound(description: $"Subscription with id {request.SubscriptionId} not found");

        var gym = await _gymsRepository.GetByIdAsync(request.GymId);
        if (gym is null)
            return Error.NotFound(description: $"Gym with id {request.SubscriptionId} not found");

        var trainer = await _trainersRepository.GetByIdAsync(request.TrainerId);
        if (trainer is null)
            return Error.NotFound(description: $"Trainer with id {request.SubscriptionId} not found");
        
        await _gymsRepository.UpdateAsync(gym);
        await _unitOfWork.CommitChangesAsync();

        return trainer;
    }
}