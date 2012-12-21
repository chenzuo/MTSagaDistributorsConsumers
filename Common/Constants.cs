using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public static class Constants
    {
        public const string WcfServiceAddress1 = "http://localhost:9999/Workflow1";
        public const string WcfServiceAddress2 = "http://localhost:9999/Workflow2";

        public const string ConnectionString = "server=localhost;database=SagaDistributorsConsumers;Trusted_Connection=Yes";
        
        public const string QueueSubscriptions = "msmq://localhost/mt_subscriptions";
        public const string QueueTimeouts = "msmq://localhost/mt_timeouts";
        public const string QueueHealth = "msmq://localhost/mt_health";

        public const string QueueWorkflow1 = "msmq://localhost/workflow1?tx=true";
        public const string QueueWorkflow2 = "msmq://localhost/workflow2?tx=true";

        public const string QueueConsumer1 = "msmq://localhost/consumer1?tx=true";
        public const string QueueConsumer2 = "msmq://localhost/consumer2?tx=true";

    }
}
