using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Workflow.Common
{
    [ServiceContract]
    public interface IWorkflowService
    {
        [OperationContract]
        WorkflowResponse StartWorkflow(WorkflowRequest request);
    }
}
