using ErrorOr;
using GymApp.Domain.GymAggregate;
using MediatR;

namespace GymApp.Application.Gyms.Queries.GetGymById;

public record GetGymByIdQuery(Guid SubscriptionId, Guid GymId) : IRequest<ErrorOr<Gym>>;