using System.Reflection;
using GymApp.Application.Common.Interfaces;
using GymApp.Domain.AdminAggregate;
using GymApp.Domain.Common;
using GymApp.Domain.Common.ValueObjects;
using GymApp.Domain.GymAggregate;
using GymApp.Domain.RoomAggregate;
using GymApp.Domain.SubscriptionAggregate;
using GymApp.Domain.TrainerAggregate;
using GymApp.Infrastructure.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GymApp.Infrastructure.Common;

public class GymManagementDbContext : DbContext, IUnitOfWork
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Gym> Gyms { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<Admin> Admins { get; set; }

    public GymManagementDbContext(
        IHttpContextAccessor httpContextAccessor,
        DbContextOptions options
    ) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task CommitChangesAsync()
    {
        await SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var domainEvents = ChangeTracker.Entries<AggregateRoot>()
            .Select(entry => entry.Entity.PopDomainEvents())
            .SelectMany(x => x)
            .ToList();

        var result = await base.SaveChangesAsync(cancellationToken);

        var domainEventsQueue =
            _httpContextAccessor.HttpContext.Items.TryGetValue(EventualConsistencyMiddleware.DomainEventsKey,
                out var value)
            && value is Queue<IDomainEvent> existingDomainEvents
                ? existingDomainEvents
                : [];
        
        domainEvents.ForEach(domainEventsQueue.Enqueue);
        _httpContextAccessor.HttpContext.Items[EventualConsistencyMiddleware.DomainEventsKey] = domainEventsQueue;
        return result;
    }
}