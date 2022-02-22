using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{

    public class Context : DbContext
    {

        public Context(
            DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Customer> Customers => Set<Customer>();


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        break;
                }
            }

            //var events = ChangeTracker.Entries<IHasDomainEvent>()
            //        .Select(x => x.Entity.DomainEvents)
            //        .SelectMany(x => x)
            //        .Where(domainEvent => !domainEvent.IsPublished)
            //        .ToArray();

            var result = await base.SaveChangesAsync(cancellationToken);

            //await DispatchEvents(events);

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        //private async Task DispatchEvents(DomainEvent[] events)
        //{
        //    foreach (var @event in events)
        //    {
        //        @event.IsPublished = true;
        //        await _domainEventService.Publish(@event);
        //    }
        //}
    }
}