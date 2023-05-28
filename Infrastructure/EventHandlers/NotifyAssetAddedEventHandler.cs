using Domain.Assets.Aggregates.Events;
using Shared.DomainEvent.Handler;

namespace Infrastructure.EventHandlers
{
    public class NotifyAssetAddedEventHandler : IDomainEventHandler<NotifyAssetAdded>
    {
        public void Handle(NotifyAssetAdded domainEvent)
        {
            Console.WriteLine("Mail aqui");
        }
    }
}
