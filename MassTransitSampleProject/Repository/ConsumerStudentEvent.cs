using GreenPipes;
using MassTransit;
using MessageContracts;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace MassTransitSampleProject.Repository
{
    public class ConsumerStudentEvent : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //Dinlenmesi gereken kuyruk tanımı yapılır  ve gelen veriye istinaden iş yapacak parçacık tanımlanır.
            //burası bizim işlem bildirimimizi alacak belki kullanıcıya bildirim atmak için kullanabilirsiniz
            var bus = BusConfigurator.ConfigureBus((cfg, host) =>
            {
                cfg.ReceiveEndpoint(RabbitMqConsts.StudentNotificationServiceQueue, e =>
                {
                    //MassTransit mesajlarda kayba uğramamak ile birlikte queue akışını da bozmamak adına hata aldığı mesajları, [hata aldığı queue ismi].error adında bir queue oluşturup buraya atmaktadır. Biz hatanın geçici olduğunu biliyoruz ve belirtiğimiz miktar ve aralıkta denemesini istiyor isek bu durumda UseMessageRetry extension methodunu kullanbiliyoruz. 
                    // retry pattern olarak search edilip detay bulunabilir
                    //lgili işlemi yapmam için 5 defa deneyecektir. Hata devam ederise mesahı error queue’a atıp bir sonrakine devam edecektir.

                    e.Consumer<StudentEventConsumer>();
                    e.UseMessageRetry(r => r.Immediate(5));
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
