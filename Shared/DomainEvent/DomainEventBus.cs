using Shared.DomainEvent.Handler;

namespace Shared.DomainEvent
{
    public class DomainEventBus : IDomainEventBus
    {
        private readonly IDomainEventHandlerFactory _domainEventHandlerFactory;

        public DomainEventBus(IDomainEventHandlerFactory domainEventHandlerFactory)
        {
            _domainEventHandlerFactory = domainEventHandlerFactory;
        }

        public async Task Execute<T>(T DomainEvent) where T : IDomainEvent
        {
            var handlers = _domainEventHandlerFactory.GetHandlers<T>();
            foreach (var handler in handlers)
            {
                handler.Handle(DomainEvent);
            }
            //await Task.Run(() =>
            //{
               
            //}
            
            //);
        }
    }
}
