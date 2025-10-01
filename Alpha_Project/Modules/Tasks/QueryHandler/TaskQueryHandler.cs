using Alpha_Project.Modules.Tasks.Dto;
using Alpha_Project.Modules.Tasks.Query;
using Domain.Interfaces;
using Shared.Base.Domain.Mediator;

namespace Alpha_Project.Modules.Tasks.QueryHandler
{
    public class TaskQueryHandler : 
        IQueryHandler<GetAllTasksQuery, IEnumerable<TaskDto>>,
        IQueryHandler<GetTaskByIdQuery, TaskDto>
    {
        private readonly ITaskRepository _repository;
        public TaskQueryHandler(ITaskRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<TaskDto>> Handle(GetAllTasksQuery query, CancellationToken cancellationToken)
        {
            var tasks = await _repository.GetAllAsync();

            return tasks.Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Status = t.Status,
                DueDate = t.DueDate
            });
        }
        public async Task<TaskDto> Handle(GetTaskByIdQuery query, CancellationToken cancellationToken)
        {
            var task = await _repository.GetByIdAsync(query.Id);
            if (task == null) return null;

            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Status = task.Status,
                DueDate = task.DueDate
            };
        }
    }

}
    