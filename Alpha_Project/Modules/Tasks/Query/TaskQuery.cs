using Alpha_Project.Modules.Tasks.Dto;
using Shared.Base.Domain.Mediator;

namespace Alpha_Project.Modules.Tasks.Query
{
    public class TaskQuery { }
    public class GetAllTasksQuery : IQuery<IEnumerable<TaskDto>> { }
    public class GetTaskByIdQuery : IQuery<TaskDto>
    {
        public Guid Id { get; set; }
    }
}
