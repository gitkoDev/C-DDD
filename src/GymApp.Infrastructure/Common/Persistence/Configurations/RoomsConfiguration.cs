using GymApp.Domain.RoomAggregate;
using GymApp.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymApp.Infrastructure.Common.Persistence.Configurations;

public class RoomsConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .ValueGeneratedNever();

        builder.Property("_maxDailySessions")
            .HasColumnName("MaxDailySessions");

        builder.Property<Dictionary<DateOnly, List<Guid>>>("_sessionsIdsByDate")
            .HasColumnName("SessionIdsByDate")
            .HasValueJsonConverter();

        // builder.OwnsOne<Schedule>("_schedule", sb =>
        // {
        //     sb.Property()
        // });

        builder.Property(r => r.Name);
        
        builder.Property(r => r.GymId);
    }
}