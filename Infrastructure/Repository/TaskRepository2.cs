using Domain.Entities;
using Domain.Interfaces;
using Shared.Base.Infra.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class TaskRepository2 : BaseRepository2<TaskEntity>, ITaskRepository
    {
        public TaskRepository2(PrimaryDbContext writeContext, ReadonlyDbContext readContext)
            : base(writeContext, readContext)
        {
        }
    }
}
