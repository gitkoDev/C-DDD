using ErrorOr;
using GymApp.Domain.AdminAggregate;
using GymApp.Domain.TrainerAggregate;
using MediatR;

namespace GymApp.Application.Gyms.Commands.AddTrainer;

public record AddTrainerCommand(Guid SubscriptionId, Guid GymId, Guid TrainerId) : IRequest<ErrorOr<Trainer>>;
