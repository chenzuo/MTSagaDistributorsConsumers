using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Common;
using MassTransit;
using Messages;
using Messages.Interfaces;

namespace Consumer.Common
{
    public class Consumer : Consumes<IWorkingStartMessage>.Context
    {
        public void Consume(IConsumeContext<IWorkingStartMessage> ctx)
        {
            for (int i = 1; i < 6; i++)
            {
                Utils.WriteToConsole(string.Format("Working on UserID: {0} for {1} seconds", ctx.Message.UserId, i), ConsoleColor.Yellow);
                Thread.Sleep(1000);
            }
            Utils.WriteToConsole(string.Format("Completed working on UserId: {0}", ctx.Message.UserId), ConsoleColor.Green);

            ctx.Bus.Publish<IWorkingCompleteMessage>(new WorkingCompleteMessage()
                                                         {
                                                             CorrelationId =
                                                                 ctx.Message.CorrelationId,
                                                             UserId = ctx.Message.UserId
                                                         });
        }
    }
}
