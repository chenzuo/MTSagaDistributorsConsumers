using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Magnum.StateMachine;
using MassTransit;
using MassTransit.Exceptions;
using MassTransit.Saga;
using Messages;
using Messages.Interfaces;

namespace Workflow.Common
{
    public class WorkflowSaga : SagaStateMachine<WorkflowSaga>, ISaga
    {
        static WorkflowSaga()
        {
            Define(() =>
                       {
                           Initially(
                               When(BeginWorkflow)
                               .Then((saga, message) => saga.Handle(message))
                               .TransitionTo(Working)
                                );

                           During(Working,
                               When(WorkingComplete)
                               .Then((saga, message) => saga.Handle(message))
                               .TransitionTo(Completed)
                               );
                       });
        }

        public WorkflowSaga()
        {
        }

        public WorkflowSaga(Guid correlationId)
        {
            this.CorrelationId = correlationId;
        }

        public virtual int UserId { get; set; }
        public virtual Guid CorrelationId { get; set; }
        public virtual IServiceBus Bus { get; set; }

        public static State Initial { get; set; }
        public static State Working { get; set; }
        public static State Completed { get; set; }

        public static Event<IBeginWorkflowMessage> BeginWorkflow { get; set; }
        public static Event<IWorkingCompleteMessage> WorkingComplete { get; set; }

        public void Handle(IBeginWorkflowMessage message)
        {
            Utils.WriteToConsole(string.Format("Starting Saga for UserID: {0}", message.UserId), ConsoleColor.Cyan);
            this.UserId = message.UserId;
            Bus.Publish(new WorkingStartMessage{ CorrelationId = this.CorrelationId, UserId = this.UserId});
        }

        public void Handle(IWorkingCompleteMessage message)
        {
            Utils.WriteToConsole(string.Format("Finised saga for UserID: {0}", message.UserId), ConsoleColor.Green);
        }
    }
}
