using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MassTransit;

namespace Messages.Interfaces
{
    public interface IBeginWorkflowMessage : CorrelatedBy<Guid>
    {
        int UserId { get; set; }
    }
}
