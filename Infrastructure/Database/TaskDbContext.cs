using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Base.Domain.Mediator;
using Shared.Base.Infra.Common.Base;
using Shared.Base.Infra.Common.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database
{
    public class TaskPrimaryDbContext : PrimaryDbContext
    {
        public TaskPrimaryDbContext(DbContextOptions<TaskPrimaryDbContext> options)
            : base(options) { }

        public DbSet<TaskEntity> Tasks { get; set; }
    }

    public class TaskReadonlyDbContext : ReadonlyDbContext
    {
        public TaskReadonlyDbContext(DbContextOptions<TaskReadonlyDbContext> options)
            : base(options) { }

        public DbSet<TaskEntity> Tasks { get; set; }
    }
}
