using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Base.Share.Interface
{
    public interface IUpdateTracking
    {
        DateTimeOffset? UpdatedAt { get; set; }
        Guid? UpdatedBy { get; set; }
    }
}
