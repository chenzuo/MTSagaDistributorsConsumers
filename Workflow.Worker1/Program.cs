using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using Common;
using MassTransit;
using MassTransit.NHibernateIntegration.Saga;
using Messages.Interfaces;
using NHibernate.Dialect;
using NHibernate.Driver;
using Workflow.Common;

namespace Workflow.Worker1
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri baseAddress = new Uri(Constants.WcfServiceAddress1);

            using (ServiceHost host = new ServiceHost(typeof(WorkflowService), baseAddress))
            {
                // Enable metadata publishing.
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                host.Description.Behaviors.Add(smb);

                // Open the ServiceHost to start listening for messages. Since
                // no endpoints are explicitly configured, the runtime will create
                // one endpoint per base address for each service contract implemented
                // by the service.
                host.Open();

                var p = new MassTransit.NHibernateIntegration.NHibernateSessionFactoryProvider(
                        new[] { typeof(WorkflowSagaMap) }, m =>
                        {
                            m.ConnectionString = Constants.ConnectionString;
                            m.Dialect<MsSql2008Dialect>();
                            m.Driver<Sql2008ClientDriver>();
                        });

                var sf = p.GetSessionFactory();
                
                Bus.Initialize(m =>
                                   {
                                       Utils.SetDefaultBusSettings(m);
                                       m.ReceiveFrom(Constants.QueueWorkflow2);

                                       m.Distributor(d =>
                                                         {
                                                             d.Handler<IBeginWorkflowMessage>();
                                                             d.Handler<IWorkingStartMessage>();
                                                         });

                                       m.Worker(s =>
                                                       {
                                                           s.Saga<WorkflowSaga>(new NHibernateSagaRepository<WorkflowSaga>(sf));
                                                       });
                                   });

           
                Utils.WriteToConsole(string.Format("The service is ready at: {0}", baseAddress), ConsoleColor.Cyan);
                Utils.WriteToConsole("Press <Enter> to stop the service.");
                Console.ReadLine();

                // close the service host
                host.Close();
            }
        }
    }
}
