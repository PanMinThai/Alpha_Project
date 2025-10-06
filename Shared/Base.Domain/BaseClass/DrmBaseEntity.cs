using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Base.Domain.BaseClass
{
    public abstract class DrmBaseEntity : CreationTrackedEntity
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
