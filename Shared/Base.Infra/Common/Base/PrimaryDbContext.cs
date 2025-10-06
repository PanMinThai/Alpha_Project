using Microsoft.EntityFrameworkCore;
using Shared.Base.Infra.Common.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Base.Infra.Common.Base
{
    public class PrimaryDbContext : BaseDbContext
    {
        public PrimaryDbContext(DbContextOptions options, ContextScanSettings? contextScanSettings = null)
            : base(options, contextScanSettings)
        {
        }

    }

}
