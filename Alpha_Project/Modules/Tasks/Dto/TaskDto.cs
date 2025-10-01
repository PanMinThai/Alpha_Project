using Domain.ValueObjects;

namespace Alpha_Project.Modules.Tasks.Dto
{
    public class TaskDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public AppTaskStatus Status { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
