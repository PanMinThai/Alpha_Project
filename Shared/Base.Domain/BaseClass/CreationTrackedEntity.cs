using Shared.Base.Share.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Base.Domain.BaseClass
{
    public abstract class CreationTrackedEntity : IdEntity<Guid>, ICreationTracking
    {
        public DateTimeOffset CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
