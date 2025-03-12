namespace GymApp.Contracts.Gyms;

public record AddTrainerRequest(Guid TrainerId, Guid SubscriptionId);