using GreenPipes;
using MassTransit;
using MessageContracts;
using System;

namespace ThirdPartyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Registration";
            Console.Title = "ThirdParty";
            var bus = BusConfigurator.ConfigureBus((cfg, host) =>
            {
                cfg.ReceiveEndpoint(RabbitMqConsts.StudentThirdPartyServiceQueue, e =>
                {
                    e.Consumer<StudentEventConsumer>();
                    // RateLimit middlewere ile belirli süre içersinde işlenecek mesaj adeti verebiliyoruz. Servise 1 dk içerisnde 1000 request yapabileceğimizi söyledik
                    e.UseRateLimit(1000, TimeSpan.FromMinutes(1));
                });
            });
            bus.StartAsync();
            Console.WriteLine("Listening for student registered events.. Press enter to exit");
            Console.ReadLine();
            bus.StopAsync();
        }
    }
}
