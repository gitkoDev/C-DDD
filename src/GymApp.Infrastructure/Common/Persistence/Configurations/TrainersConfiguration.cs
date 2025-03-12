using GymApp.Domain.Common.Entities;
using GymApp.Domain.Common.ValueObjects;
using GymApp.Domain.TrainerAggregate;
using GymApp.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymApp.Infrastructure.Common.Persistence.Configurations;

public class TrainersConfiguration : IEntityTypeConfiguration<Trainer>
{
    public void Configure(EntityTypeBuilder<Trainer> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .ValueGeneratedNever();

        builder.Property(t => t.UserId);

        builder.Property<List<Guid>>("_sessionIds")
            .HasColumnName("SessionIds")
            .HasListOfIdsConverter();

        builder.OwnsOne<Schedule>("_schedule", sb =>
            {
                sb.Property<Dictionary<DateOnly, List<TimeRange>>>("_calendar")
                    .HasColumnName("ScheduleCalendar")
                    .HasValueJsonConverter();

                sb.Property(s => s.Id)
                    .HasColumnName("ScheduleId");
            }
        );
    }
}