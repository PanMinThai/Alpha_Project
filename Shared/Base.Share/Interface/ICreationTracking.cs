using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Base.Share.Interface
{
    public interface ICreationTracking
    {
        DateTimeOffset CreatedAt { get; set; }
        Guid CreatedBy { get; set; }
    }
}
