namespace Ordering.Domain.Abstractions
{
    /// <summary>
    /// The generic IEntity<T> ensures strong typing for the Id property. 
    /// This is especially useful in systems where entities can have different types of identifiers:
    ///     Order entity might use a Guid as Id.
    ///     Customer entity might use an int as Id.
    ///     Product entity might use a string as Id.
    /// By using a generic, you ensure compile-time safety and prevent accidental mixing of identifiers.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntity<T> : IEntity
    {
        public T Id { get; set; }
    }

    /// <summary>
    /// Sometimes, you might deal with scenarios where the type of Id isn't relevant or the entity does not have an Id
    /// In this case, you don't need IEntity<T>. Instead, IEntity suffices, simplifying the model.
    /// </summary>
    public interface IEntity
    {
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
