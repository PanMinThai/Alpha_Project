using Shared.Base.Domain.DomainEvent;
using Shared.Base.Share.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Base.Domain.BaseClass
{
    public abstract class IdEntity <TKey> : IEntity
    {
        public TKey Id { get; set; }
        private readonly List<IEvent> _domainEvents = [];
        [NotMapped]
        public virtual IReadOnlyCollection<IEvent> DomainEvents => _domainEvents;

        public virtual void AddDomainEvent(IEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public virtual void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
