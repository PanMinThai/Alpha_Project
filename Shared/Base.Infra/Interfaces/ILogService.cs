using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Base.Infra.Interfaces
{
    public interface ILogService
    {
        Task LogAsync(string message);
    }
}
