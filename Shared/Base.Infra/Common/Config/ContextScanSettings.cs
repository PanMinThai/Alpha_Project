using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Base.Infra.Common.Config
{
    public class ContextScanSettings(IEnumerable<Assembly>? assemblies = null, IEnumerable<Type>? types = null)
    {
        private readonly IEnumerable<Assembly> _assemblies = assemblies ?? []; //[Assembly.GetExecutingAssembly()];
        private readonly IEnumerable<Type> _types = types ?? [];
        public IEnumerable<Assembly> Assemblies => _assemblies.ToList().AsReadOnly();
        public IEnumerable<Type> Types => _types.ToList().AsReadOnly();
    }
}
