using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ordering.Domain.Abstractions;

namespace Ordering.Infrastructure.Data.Interceptors
{
    //MediatR need to be registered on application pipeline before injected here 
    public class DispatchDomainEventsInterceptor(IMediator mediator) : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
            return base.SavingChanges(eventData, result);
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            await DispatchDomainEvents(eventData.Context);
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public async Task DispatchDomainEvents(DbContext? context)
        {
            if (context == null) return;

            //Get all entities that are aggregates and contain domain event
            var aggregates = context.ChangeTracker
                .Entries<IAggregate>()
                .Where(a => a.Entity.DomainEvents.Any())
                .Select(a => a.Entity);

            //Get all domain events from the list of entities
            var domainEvents = aggregates
                .SelectMany(a => a.DomainEvents)
                .ToList();

            //Clear all domain events for each aggregate
            aggregates.ToList().ForEach(a => a.ClearDomainEvents());

            //Publish domain events
            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}
