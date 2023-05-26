namespace Shared.DomainEvent
{
    public interface IDomainEventBus
    {
        Task Execute<T>(T DomainEvent) where T : IDomainEvent;
    }
}
