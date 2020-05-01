using GreenPipes;
using MassTransit;
using MessageContracts;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RegistrationStudent
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var bus = BusConfigurator.ConfigureBus((cfg, host) =>
            {
                cfg.ReceiveEndpoint(
                    RabbitMqConsts.StudentRegisterServiceQueue, e =>
                    {
                        //Message yakalanýnca iþlem yapýcak consumer yazýlýr
                        e.Consumer<ConsumerStudent>();
                        e.UseCircuitBreaker(cb =>
                        {
                            //TripThreshold ile alýnan taleplerimizin %15'inde ve ActiveThreshold ile üst üste 10 hata alýndðýnda 5 dk beklemesi gerektiðini ResetInterval ile belirtiyoruz. TrackingPeriod ise ResetInterval süresinden sonra 1 dk daha tetikte beklemesi söylüyor. Bu sürede yine hata alýnýr ise ActiveThreshold ve TripThreshold limitlerini beklemeden yine 5 dk süre ile beklemeye geçecektir.

                            cb.TrackingPeriod = TimeSpan.FromMinutes(1);
                            cb.TripThreshold = 15;
                            cb.ActiveThreshold = 10;
                            cb.ResetInterval = TimeSpan.FromMinutes(5);

                        });
                        // RateLimit middlewere ile belirli süre içersinde iþlenecek mesaj adeti verebiliyoruz. Servise 1 dk içerisnde 1000 request yapabileceðimizi söyledik
                        //e.UseRateLimit(1000, TimeSpan.FromMinutes(1));

                    });

            });

            while (!stoppingToken.IsCancellationRequested)
            {
                await bus.StartAsync();
            }
            await bus.StopAsync();
        }
    }
}
