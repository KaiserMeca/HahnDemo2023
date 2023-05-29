namespace Shared.DomainEvent.Handler
{
    /// <summary>
    /// Represents a handler for a specific domain event type.
    /// </summary>
    /// <typeparam name="T">The type of the domain event.</typeparam>
    public interface IDomainEventHandler<T> where T : IDomainEvent
    {
        /// <summary>
        /// Handles the domain event.
        /// </summary>
        /// <param name="domainEvent">The domain event to handle.</param>
        void Handle(T domainEvent);
    }
}
