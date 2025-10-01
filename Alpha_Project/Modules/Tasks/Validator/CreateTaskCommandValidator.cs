using Alpha_Project.Modules.Tasks.Command;
using Shared.Base.API.Validation;

namespace Alpha_Project.Modules.Tasks.Validator
{
    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator()
        {
            RuleFor(x => !string.IsNullOrWhiteSpace(x.Title), "Title is required.");
            RuleFor(x => x.DueDate > DateTime.UtcNow, "DueDate must be in the future.");
        }
    }
}
