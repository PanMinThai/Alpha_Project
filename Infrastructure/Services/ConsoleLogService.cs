using Shared.Base.Infra.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ConsoleLogService : ILogService
    {
        public Task LogAsync(string message)
        {
            Console.WriteLine($"[LOG] {DateTime.UtcNow:u} - {message}");
            return Task.CompletedTask;
        }
    }
}
