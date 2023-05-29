namespace Shared.DomainEvent
{
    /// <summary>
    /// Represents a domain event bus.
    /// </summary>
    public interface IDomainEventBus
    {
        /// <summary>
        /// Executes the specified domain event asynchronously.
        /// </summary>
        /// <typeparam name="T">The type of the domain event.</typeparam>
        /// <param name="DomainEvent">The domain event to execute.</param>
        Task Execute<T>(T DomainEvent) where T : IDomainEvent;
    }
}
