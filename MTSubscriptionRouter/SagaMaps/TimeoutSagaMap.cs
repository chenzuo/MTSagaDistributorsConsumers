using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MassTransit.NHibernateIntegration;
using MassTransit.Services.Timeout.Server;

namespace MTSubscriptionRouter.SagaMaps
{
    public class TimeoutSagaMap : SagaStateMachineClassMapping<TimeoutSaga>
    {
        public TimeoutSagaMap()
        {
            Property(x => x.TimeoutId);
            Property(x => x.Tag);

            Property(x => x.TimeoutAt);
        }
    }
}
