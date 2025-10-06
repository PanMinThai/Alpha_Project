using Shared.Base.Domain.DomainEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Base.Domain.BaseClass.DomainEvent
{
    public class SoftDeletedDomainEvent : IEvent
    {
        public required Type Type { get; init; }
        public required Guid Id { get; init; }
    }
}
