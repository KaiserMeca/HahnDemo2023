namespace Shared.DomainEvent.Handler
{
    /// <summary>
    /// Represents a factory for creating domain event handlers.
    /// </summary>
    public interface IDomainEventHandlerFactory
    {
        /// <summary>
        /// Gets the collection of handlers for a specific domain event type.
        /// </summary>
        /// <typeparam name="T">The type of the domain event.</typeparam>
        /// <returns>The collection of domain event handlers.</returns>
        IEnumerable<IDomainEventHandler<T>> GetHandlers<T>() where T : IDomainEvent;
    }
}
