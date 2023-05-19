namespace Common.Model
{
    public interface IEventProvider
    {
        IEnumerable<IDomainEvent> GetUncommittedDomainEvents();
        void MarkDomainEventsAsCommitted();
    }
}
