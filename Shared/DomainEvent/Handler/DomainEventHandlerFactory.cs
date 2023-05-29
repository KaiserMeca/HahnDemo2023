using Microsoft.Extensions.DependencyInjection;

namespace Shared.DomainEvent.Handler
{
    /// <summary>
    /// Factory for domain event handlers.
    /// </summary>
    public class DomainEventHandlerFactory : IDomainEventHandlerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the DomainEventHandlerFactory class.
        /// </summary>
        /// <param name="serviceProvider">Service provider for dependency resolution.</param>
        public DomainEventHandlerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Gets the domain event handlers for a specific event type.
        /// </summary>
        /// <typeparam name="T">Event type.</typeparam>
        /// <returns>Collection of domain event handlers.</returns>
        public IEnumerable<IDomainEventHandler<T>> GetHandlers<T>() where T : IDomainEvent
        {
            return _serviceProvider.GetService<IEnumerable<IDomainEventHandler<T>>>();
        }
    }
}
