using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MassTransit;

namespace Messages.Interfaces
{
    public interface IWorkingCompleteMessage : CorrelatedBy<Guid>
    {
        int UserId { get; set; }
    }
}
