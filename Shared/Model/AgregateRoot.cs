using Shared.DomainEvent;

namespace Shared.Model
{
    /// <summary>
    /// Represents an aggregate root.
    /// </summary>
    public abstract class AgregateRoot : IEventProvider
    {
        private static List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

        /// <summary>
        /// Initializes a new instance of the <see cref="AgregateRoot"/> class.
        /// </summary>
        protected AgregateRoot()
        {

        }

        /// <summary>
        /// Gets the uncommitted domain events.
        /// </summary>
        /// <returns>An enumeration of uncommitted domain events.</returns>
        public IEnumerable<IDomainEvent> GetUncommittedDomainEvents()
        {
            return _domainEvents;
        }

        /// <summary>
        /// Marks the domain events as committed.
        /// </summary>
        public void MarkDomainEventsAsCommitted()
        {
            _domainEvents.Clear();
        }

        /// <summary>
        /// Adds a domain event.
        /// </summary>
        /// <param name="eve">The domain event to add.</param>
        protected void AddDomainEvent(IDomainEvent eve)
        {
            _domainEvents.Add(eve);
        }
    }
}
