using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MassTransit.NHibernateIntegration;
using MassTransit.Services.Subscriptions.Server;

namespace MTSubscriptionRouter.SagaMaps
{
    public class SubscriptionClientSagaMap : SagaStateMachineClassMapping<SubscriptionClientSaga>
    {
        public SubscriptionClientSagaMap()
        {
            Property(x => x.ControlUri, x => x.Type<UriUserType>());
            Property(x => x.DataUri, x => x.Type<UriUserType>());
        }
    }
}
