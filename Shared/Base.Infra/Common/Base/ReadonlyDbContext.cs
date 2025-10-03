using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Base.Infra.Common.Base
{
    public class ReadonlyDbContext : BaseDbContext
    {
        public ReadonlyDbContext(DbContextOptions options) : base(options) { }

        public DbSet<TaskEntity> Task { get; set; }
        public DbSet<WorkFlowRequestEntity> WorkFlowRequest { get; set; }
    }
}
