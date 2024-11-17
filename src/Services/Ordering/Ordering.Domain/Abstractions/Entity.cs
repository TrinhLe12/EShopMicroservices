
namespace Ordering.Domain.Abstractions
{
    /// <summary>
    /// Implement shared behavior and logic 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Entity<T> : IEntity<T>
    {
        public T Id { get; set; } = default!;
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
