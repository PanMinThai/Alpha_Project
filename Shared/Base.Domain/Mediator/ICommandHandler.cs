using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Base.Domain.Mediator
{
    public interface ICommandHandler<TCommand, TReply>
        where TCommand : ICommand<TReply>
    {
        Task<TReply> Handle(TCommand command, CancellationToken cancellationToken);
    }
}
