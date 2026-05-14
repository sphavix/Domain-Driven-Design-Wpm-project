using System;
using System.Collections.Generic;
using System.Text;
using Wpm.SharedKernel.DomainEvents;

namespace Wpm.SharedKernel
{
    public abstract class AggregateRoot : BaseEntity
    {
        private readonly List<IDomainEvent> changes = new();

        public int Version { get; private set; }

        public IReadOnlyCollection<IDomainEvent> GetChanges()
        {
            return changes.AsReadOnly();
        }

        public void ClearChanges()
        {
            changes.Clear();
        }

        protected void ApplyDomainEvent(IDomainEvent domainEvent)
        {
            ChangeStateByUsingDomainEvent(domainEvent);
            changes.Add(domainEvent);
            Version++;
        }

        public void Load(IEnumerable<IDomainEvent> history)
        {
            foreach (var domainEvent in history)
            {
               ApplyDomainEvent(domainEvent);
            }
            ClearChanges();
        }

        public abstract void ChangeStateByUsingDomainEvent(IDomainEvent domainEvent);


    }
}
