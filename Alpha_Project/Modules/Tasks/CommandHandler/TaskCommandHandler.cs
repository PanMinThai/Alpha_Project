using Alpha_Project.Modules.Tasks.Command;
using Domain.Entities;
using Domain.Interfaces;
using Shared.Base.Domain.Mediator;

namespace Alpha_Project.Modules.Tasks.CommandHandler
{
    public class TaskCommandHandler :
        ICommandHandler<CreateTaskCommand, Guid>,
        ICommandHandler<UpdateTaskCommand, Guid>,
        ICommandHandler<DeleteTaskCommand, Guid>
    {
        private readonly ITaskRepository _taskRepository;

        public TaskCommandHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<Guid> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
        {
            var task = new TaskEntity(command.Title, command.DueDate);

            await _taskRepository.AddAsync(task);

            return task.Id;
        }

        public async Task<Guid> Handle(UpdateTaskCommand command, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(command.Id);
            if (task == null) throw new Exception("Task not found");

            task.Update(command.Title, command.DueDate, command.Status);

            await _taskRepository.UpdateAsync(task);
            return task.Id;
        }

        public async Task<Guid> Handle(DeleteTaskCommand command, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(command.Id);
            if (task == null) throw new Exception("Task not found");

            await _taskRepository.DeleteAsync(task.Id);
            return task.Id;
        }
    }
}
