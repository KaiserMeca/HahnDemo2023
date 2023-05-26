﻿using Shared.DomainEvent;

namespace Shared.Model
{
    public interface IEventProvider
    {
        IEnumerable<IDomainEvent> GetUncommittedDomainEvents();
        void MarkDomainEventsAsCommitted();
    }
}
