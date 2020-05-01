using MassTransit;
using MassTransit.RabbitMqTransport;
using System;

namespace MessageContracts
{
    public static class BusConfigurator
    {
        //nuget üzerinden MassTransit ve MassTransit.Rabbitmq kurunuz
        public static IBusControl ConfigureBus(Action<IRabbitMqBusFactoryConfigurator, IRabbitMqHost> registrationAction = null)
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(RabbitMqConsts.RabbitMqUri), hst =>
                {
                    hst.Username(RabbitMqConsts.UserName);
                    hst.Password(RabbitMqConsts.Password);
                });

                registrationAction?.Invoke(cfg, host);
            });
        }
    }
}
