using MediatR;

namespace Ordering.Domain.Abstractions
{
    /// <summary>
    /// MediatR abstracts the complexity of dispatching events to their respective handlers.
    /// By inheriting INotification, any domain event becomes a MediatR notification, 
    /// and its handlers are automatically resolved by the MediatR pipeline
    /// </summary>
    public interface IDomainEvent : INotification
    {
        Guid? EventId => Guid.NewGuid();
        public DateTime? OccurredOn => DateTime.UtcNow;
        public string? EventType => GetType().AssemblyQualifiedName;
    }
}
