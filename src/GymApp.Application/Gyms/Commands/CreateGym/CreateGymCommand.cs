using ErrorOr;
using GymApp.Domain.GymAggregate;
using MediatR;

namespace GymApp.Application.Gyms.Commands.CreateGym;

public record CreateGymCommand(Guid SubscriptionId, string GymName) : IRequest<ErrorOr<Gym>>;