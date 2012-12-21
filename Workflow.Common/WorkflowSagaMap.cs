using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode;

namespace Workflow.Common
{
    public class WorkflowSagaMap : MassTransit.NHibernateIntegration.SagaClassMapping<WorkflowSaga>
    {
        public WorkflowSagaMap()
        {
            base.Property(m => m.UserId);
            base.Property(m => m.CurrentState, pm =>
            {
                pm.Type<MassTransit.NHibernateIntegration.StateMachineUserType>();
                pm.Access(Accessor.Field);
            });
        }
    }
}
