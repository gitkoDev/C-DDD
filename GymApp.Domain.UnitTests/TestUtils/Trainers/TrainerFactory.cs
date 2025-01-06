namespace GymApp.Domain.UnitTests.TestUtils.Trainers;

public static class TrainerFactory
{
    public static Trainer CreateTrainer(Guid? id = null)
    {
        return new Trainer(id: id ?? Guid.NewGuid());
    }
}