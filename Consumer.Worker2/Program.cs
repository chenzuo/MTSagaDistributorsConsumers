using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using MassTransit;
using Messages.Interfaces;

namespace Consumer.Worker2
{
    class Program
    {
        static void Main(string[] args)
        {
            Bus.Initialize(m =>
            {
                Utils.SetDefaultBusSettings(m);
                m.ReceiveFrom(Constants.QueueConsumer2);

                m.Distributor(d => d.Handler<IWorkingCompleteMessage>());
                m.Worker(c => c.Consumer<Common.Consumer>());
            });

            Utils.WriteToConsole("Consumer2 ready", ConsoleColor.Green);
            Utils.WriteToConsole("Press <Enter> to stop the service.");
            Console.ReadLine();
        }
    }
}
