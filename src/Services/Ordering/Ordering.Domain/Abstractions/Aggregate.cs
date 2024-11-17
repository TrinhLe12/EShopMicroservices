
namespace Ordering.Domain.Abstractions
{
    /// <summary>
    /// Implement shared behavior and logic. 
    /// Appgerate root is Entity in nature --> need all properties of Entity, 
    /// including logics that implemented in Entity abstract class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Aggregate<T> : Entity<T>, IAggregate<T>
    {
        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public IDomainEvent[] ClearDomainEvents()
        {
            IDomainEvent[] dequeuedEvents = _domainEvents.ToArray();

            _domainEvents.Clear();

            return dequeuedEvents;
        }
    }
}
