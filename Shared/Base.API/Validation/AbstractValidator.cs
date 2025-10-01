using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Base.API.Validation
{
    public abstract class AbstractValidator<T> : IValidator<T>
    {
        private readonly List<Func<T, string?>> _rules = new();

        protected void RuleFor(Func<T, bool> predicate, string errorMessage)
        {
            _rules.Add(x => predicate(x) ? null : errorMessage);
        }

        public List<string> Validate(T instance)
        {
            return _rules
                .Select(rule => rule(instance))
                .Where(msg => msg != null)
                .Cast<string>()
                .ToList();
        }
    }
}
