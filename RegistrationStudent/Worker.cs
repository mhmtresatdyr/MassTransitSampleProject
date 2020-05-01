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
                        //Message yakalan�nca i�lem yap�cak consumer yaz�l�r
                        e.Consumer<ConsumerStudent>();
                        e.UseCircuitBreaker(cb =>
                        {
                            //TripThreshold ile al�nan taleplerimizin %15'inde ve ActiveThreshold ile �st �ste 10 hata al�nd��nda 5 dk beklemesi gerekti�ini ResetInterval ile belirtiyoruz. TrackingPeriod ise ResetInterval s�resinden sonra 1 dk daha tetikte beklemesi s�yl�yor. Bu s�rede yine hata al�n�r ise ActiveThreshold ve TripThreshold limitlerini beklemeden yine 5 dk s�re ile beklemeye ge�ecektir.

                            cb.TrackingPeriod = TimeSpan.FromMinutes(1);
                            cb.TripThreshold = 15;
                            cb.ActiveThreshold = 10;
                            cb.ResetInterval = TimeSpan.FromMinutes(5);

                        });
                        // RateLimit middlewere ile belirli s�re i�ersinde i�lenecek mesaj adeti verebiliyoruz. Servise 1 dk i�erisnde 1000 request yapabilece�imizi s�yledik
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
