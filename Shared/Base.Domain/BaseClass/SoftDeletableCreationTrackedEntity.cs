using Shared.Base.Domain.BaseClass.DomainEvent;
using Shared.Base.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Base.Domain.BaseClass
{
    public abstract class SoftDeletableCreationTrackedEntity : CreationTrackedEntity, ISoftDeletable
    {
        public bool IsDeleted { get; set; }

        public virtual void SoftDelete()
        {
            IsDeleted = true;
            AddDomainEvent(new SoftDeletedDomainEvent
            {
                Type = GetType(),
                Id = Id
            });
        }
    }

}
