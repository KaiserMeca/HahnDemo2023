using Shared.DomainEvent.Handler;

namespace Shared.DomainEvent
{
    /// <summary>
    /// Represents a bus for executing domain events.
    /// </summary>
    public class DomainEventBus : IDomainEventBus
    {
        private readonly IDomainEventHandlerFactory _domainEventHandlerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEventBus"/> class.
        /// </summary>
        /// <param name="domainEventHandlerFactory">The factory for domain event handlers.</param>
        public DomainEventBus(IDomainEventHandlerFactory domainEventHandlerFactory)
        {
            _domainEventHandlerFactory = domainEventHandlerFactory;
        }

        /// <summary>
        /// Executes a domain event by invoking its associated handlers.
        /// </summary>
        /// <typeparam name="T">The type of the domain event.</typeparam>
        /// <param name="domainEvent">The domain event to execute.</param>
        public async Task Execute<T>(T domainEvent) where T : IDomainEvent
        {
            var handlers = _domainEventHandlerFactory.GetHandlers<T>();

            await Task.Run(() =>
            {
                foreach (var handler in handlers)
                {
                    handler.Handle(domainEvent);
                }
            });
        }
    }
}
