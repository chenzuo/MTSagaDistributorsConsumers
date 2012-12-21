using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MassTransit.NHibernateIntegration;
using MassTransit.Services.Subscriptions.Server;

namespace MTSubscriptionRouter.SagaMaps
{
    public class SubscriptionSagaMap : SagaStateMachineClassMapping<SubscriptionSaga>
    {
        public SubscriptionSagaMap()
        {
            Component(x => x.SubscriptionInfo, x =>
            {
                x.Property(c => c.ClientId);
                x.Property(c => c.CorrelationId, c => c.Column("MessageCorrelationId"));
                x.Property(c => c.EndpointUri, c => c.Type<UriUserType>());

                x.Property(c => c.MessageName);
                x.Property(c => c.SequenceNumber);
                x.Property(c => c.SubscriptionId);
            });
        }
    }
}
