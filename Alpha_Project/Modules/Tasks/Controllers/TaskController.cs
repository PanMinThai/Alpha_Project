using Alpha_Project.Modules.Tasks.Command;
using Alpha_Project.Modules.Tasks.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Base.Domain.Mediator;

namespace Alpha_Project.Modules.Tasks.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public TaskController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskCommand command)
        {
            var id = await _dispatcher.Send(command);
            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateTaskCommand command)
        {
            command.Id = id;
            var updatedId = await _dispatcher.Send(command);
            return Ok(updatedId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deletedId = await _dispatcher.Send(new DeleteTaskCommand { Id = id });
            return Ok(deletedId);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _dispatcher.Query(new GetAllTasksQuery());
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var task = await _dispatcher.Query(new GetTaskByIdQuery { Id = id });
            return task is null ? NotFound() : Ok(task);
        }

    }
}
