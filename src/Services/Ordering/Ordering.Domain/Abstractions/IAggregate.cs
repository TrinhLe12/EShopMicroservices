namespace Ordering.Domain.Abstractions
{
    /// <summary>
    /// Represent aggregate root, which is an entity in nature
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAggregate<T> : IAggregate, IEntity<T>
    {

    }

    /// <summary>
    /// Represent aggregate root, which is an entity in nature
    /// </summary>
    public interface IAggregate : IEntity
    {
        IReadOnlyList<IDomainEvent> DomainEvents { get; }
        IDomainEvent[] ClearDomainEvents();
    }
}
