using Shared.Base.Infra.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ConsoleEmailService : IEmailService
    {
        public Task SendEmailAsync(string to, string subject, string body)
        {
            Console.WriteLine($"[EMAIL] To: {to}, Subject: {subject}, Body: {body}");
            return Task.CompletedTask;
        }
    }
}
