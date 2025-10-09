using Alpha_Project.Modules.Tasks.Command;
using Domain.Entities;
using Domain.Interfaces;
using Shared.Base.API.Interface;
using Shared.Base.Domain.Mediator;

namespace Alpha_Project.Modules.Tasks.CommandHandler
{
    public class TaskCommandHandler :
    ICommandHandler<CreateTaskCommand, Guid>,
    ICommandHandler<UpdateTaskCommand, Guid>,
    ICommandHandler<DeleteTaskCommand, Guid>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TaskCommandHandler> _logger;

        public TaskCommandHandler(
            ITaskRepository taskRepository,
            IUnitOfWork unitOfWork,
            ILogger<TaskCommandHandler> logger)
        {
            _taskRepository = taskRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
        {
            Guid taskId = Guid.Empty;

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var task = new TaskEntity(command.Title, command.DueDate);
                await _taskRepository.AddAsync(task);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                taskId = task.Id;
                _logger.LogInformation("Task created successfully with Id: {TaskId}", taskId);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error creating task");
                throw;
            }

            return taskId;
        }

        public async Task<Guid> Handle(UpdateTaskCommand command, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var task = await _taskRepository.GetByIdAsync(command.Id);
                if (task == null)
                    throw new Exception("Task not found");

                task.Update(command.Title, command.DueDate, command.Status);
                await _taskRepository.UpdateAsync(task);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation("Task updated successfully with Id: {TaskId}", command.Id);
                return task.Id;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error updating task with Id: {TaskId}", command.Id);
                throw;
            }
        }

        public async Task<Guid> Handle(DeleteTaskCommand command, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var task = await _taskRepository.GetByIdAsync(command.Id);
                if (task == null)
                    throw new Exception("Task not found");

                await _taskRepository.DeleteAsync(task.Id);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation("Task deleted successfully with Id: {TaskId}", command.Id);
                return task.Id;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error deleting task with Id: {TaskId}", command.Id);
                throw;
            }
        }
    }

}
