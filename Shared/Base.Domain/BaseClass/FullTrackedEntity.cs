using Shared.Base.Share.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Base.Domain.BaseClass
{
    public abstract class FullTrackedEntity : CreationTrackedEntity, IUpdateTracking, IOptimisticLock
    {
        public Guid UpdateToken { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public Guid UpdatedBy { get; set; }
    }
}
