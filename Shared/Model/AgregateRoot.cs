
namespace Shared.Model
{
    public abstract class AgregateRoot : IEventProvider
    {
        private static List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

        protected AgregateRoot()
        {
            
        }

        public IEnumerable<IDomainEvent> GetUncommittedDomainEvents()
        {
            return _domainEvents;
        }

        public void MarkDomainEventsAsCommitted()
        {
            _domainEvents.Clear();
        }

        protected void AddDomainEvent(IDomainEvent eve)
        {
            _domainEvents.Add(eve);
        }
    }
}
