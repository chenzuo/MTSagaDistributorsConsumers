using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using MTSubscriptionRouter.SagaMaps;
using MassTransit;
using MassTransit.NHibernateIntegration.Saga;
using MassTransit.Services.HealthMonitoring;
using MassTransit.Services.HealthMonitoring.Server;
using MassTransit.Services.Subscriptions.Server;
using MassTransit.Services.Timeout;
using MassTransit.Services.Timeout.Server;
using NHibernate;
using NHibernate.Dialect;
using NHibernate.Driver;

namespace MTSubscriptionRouter
{
    class Program
    {
        static void Main(string[] args)
        {
            var nhibernateConfig = new MassTransit.NHibernateIntegration.NHibernateSessionFactoryProvider(
                new[] { typeof(SubscriptionClientSagaMap), typeof(SubscriptionSagaMap), typeof(TimeoutSagaMap), typeof(HealthSagaMap) }, m =>
                {
                    m.ConnectionString = Constants.ConnectionString;
                    m.Dialect<MsSql2008Dialect>();
                    m.Driver<Sql2008ClientDriver>();
                });

            var sessionFactory = nhibernateConfig.GetSessionFactory();

            InitializeSubscriptionService(sessionFactory);
            InitializeTimeoutService(sessionFactory);
            InitializeHealthService(sessionFactory);
            
            Utils.WriteToConsole("Router is running. Press enter to exit", ConsoleColor.Yellow);
            Console.ReadLine();
        }

        private static void InitializeSubscriptionService(ISessionFactory sessionFactory)
        {
            Console.Write("Initializing the subscription service... ");

            var subscriptionBus = ServiceBusFactory.New(sbc =>
            {
                sbc.UseMsmq();
                sbc.VerifyMsmqConfiguration();
                sbc.SetConcurrentConsumerLimit(1);

                sbc.ReceiveFrom(Constants.QueueSubscriptions);
            });

            var subscriptionSagas = new NHibernateSagaRepository<SubscriptionSaga>(sessionFactory);
            var subscriptionClientSagas = new NHibernateSagaRepository<SubscriptionClientSaga>(sessionFactory);

            var subscriptionService = new SubscriptionService(subscriptionBus, subscriptionSagas, subscriptionClientSagas);
            subscriptionService.Start();

            Utils.WriteToConsole("done", ConsoleColor.Green);
        }

        private static void InitializeTimeoutService(ISessionFactory sessionFactory)
        {
            Console.Write("Initializing the timeout service... ");

            var timeoutBus = ServiceBusFactory.New(sbc =>
            {
                sbc.UseMsmq();
                sbc.VerifyMsmqConfiguration();
                sbc.UseControlBus();

                sbc.ReceiveFrom(Constants.QueueTimeouts);
                sbc.UseSubscriptionService(Constants.QueueSubscriptions);
            });

            var timeoutSagaRepository = new NHibernateSagaRepository<TimeoutSaga>(sessionFactory);
            var timeoutService = new TimeoutService(timeoutBus, timeoutSagaRepository);
            timeoutService.Start();

            Utils.WriteToConsole("done", ConsoleColor.Green);
        }

        private static void InitializeHealthService(ISessionFactory sessionFactory)
        {
            Console.Write("Initializing the health service... ");

            var healthBus = ServiceBusFactory.New(sbc =>
            {
                sbc.UseMsmq();
                sbc.VerifyMsmqConfiguration();
                sbc.UseControlBus();

                sbc.ReceiveFrom(Constants.QueueHealth);
                sbc.UseSubscriptionService(Constants.QueueSubscriptions);
            });

            var healthSagaRepository = new NHibernateSagaRepository<HealthSaga>(sessionFactory);
            var healthService = new HealthService(healthBus, healthSagaRepository);
            healthService.Start();

            Utils.WriteToConsole("done", ConsoleColor.Green);
        }
    }
}
