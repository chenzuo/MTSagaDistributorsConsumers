using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MassTransit;
using MassTransit.BusConfigurators;

namespace Common
{
    public static class Utils
    {
        public static void WriteToConsole(string message, ConsoleColor color = ConsoleColor.White)
        {
            var oldcolor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = oldcolor;
        }

        public static void SetDefaultBusSettings(ServiceBusConfigurator sbc)
        {
            sbc.UseMsmq();
            sbc.VerifyMsmqConfiguration();
            sbc.SetCreateTransactionalQueues(true);
            sbc.UseSubscriptionService(Constants.QueueSubscriptions);
            sbc.UseControlBus();
            sbc.SetDefaultTransactionTimeout(TimeSpan.FromSeconds(20));
            sbc.SetConcurrentConsumerLimit(1);
        }
    }
}
