using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MassTransit.NHibernateIntegration;
using MassTransit.Services.HealthMonitoring.Server;

namespace MTSubscriptionRouter.SagaMaps
{
    public class HealthSagaMap : SagaStateMachineClassMapping<HealthSaga>
    {
        public HealthSagaMap()
        {
            Property(x => x.ControlUri, x => x.Type<UriUserType>());
            Property(x => x.DataUri, x => x.Type<UriUserType>());

            Property(x => x.LastHeartbeat);
            Property(x => x.HeartbeatIntervalInSeconds);
        }
    }
}
