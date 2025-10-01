using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Base.API.Validation
{
    public interface IValidator<T>
    {
        List<string> Validate(T instance);
    }
}
