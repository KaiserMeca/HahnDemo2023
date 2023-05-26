using Domain.Assets.Aggregates.Events;
using Shared.DomainEvent.Handler;

namespace Infrastructure.EventHandlers
{
    public class UpdateAssetDataEventHandler : IDomainEventHandler<UpdateAssetData>
    {
        public void Handle(UpdateAssetData domainEvent)
        {
            throw new NotImplementedException();
        }
    }
}
