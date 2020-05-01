using MassTransit;
using MessageContracts.Event;
using System;
using System.Threading.Tasks;

namespace ThirdPartyApp
{
    public class StudentEventConsumer : IConsumer<IStudentEvent>
    {

        public async Task Consume(ConsumeContext<IStudentEvent> context)
        {
            //Event listen edildiği yerdir
            //veri işlenir  evente göre farklı queue hareket ettirilebilir.
            // await akışa uygun yazdırmalar farklı işlem yapmalar mesaj döndürmeler kurgulanabilir
            await Console.Out.WriteLineAsync($"Thirdpary integratin done: Register Guid: {context.Message.RegisterNumber}");
        }
    }
}
