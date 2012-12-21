using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MassTransit;
using Messages;
using Messages.Interfaces;

namespace Workflow.Common
{
    public class WorkflowService : IWorkflowService
    {
        public WorkflowResponse StartWorkflow(WorkflowRequest request)
        {
            var correlationId = Guid.NewGuid();
            var message = new BeginWorkflowMessage() {CorrelationId = correlationId, UserId = request.UserId};
            var returnMessage = new WorkflowResponse() {CorrelationId = correlationId};

            Bus.Instance.Publish<IBeginWorkflowMessage>(message);
            
            return returnMessage;
        }
    }
}
