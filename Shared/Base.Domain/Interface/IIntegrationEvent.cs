using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Base.Domain.Interface
{
    public interface IIntegrationEvent
    {
        Guid Id { get; init; }
    }
}
