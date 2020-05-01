using MassTransit;
using MessageContracts.Event;
using System.Threading.Tasks;

namespace MassTransitSampleProject.Repository
{
    public class StudentEventConsumer : IConsumer<IStudentEvent>
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public Task Consume(ConsumeContext<IStudentEvent> context)
        {
            logger.Info("Consume StudentEventConsumer");
            logger.Info("Listen Event:{context}", context);
            //Event listen edildiği yerdir
            //veri işlenir  evente göre farklı queue hareket ettirilebilir.
            logger.Info("veri işlendi");
            // await akışa uygun yazdırmalar farklı işlem yapmalar mesaj döndürmeler kurgulanabilir
            return Task.FromResult(context.Message.RegisterNumber);
        }
    }
}
