using Shared.DomainEvent;

namespace Shared.Model
{
    /// <summary>
    /// Represents an interface for an event provider.
    /// </summary>
    public interface IEventProvider
    {
        /// <summary>
        /// Gets the collection of uncommitted domain events.
        /// </summary>
        /// <returns>The collection of uncommitted domain events.</returns>
        IEnumerable<IDomainEvent> GetUncommittedDomainEvents();

        /// <summary>
        /// Marks all the domain events as committed.
        /// </summary>
        void MarkDomainEventsAsCommitted();
    }
}
