using MassTransit;
using MessageContracts.Command;
using MessageContracts.Event;
using System;
using System.Threading.Tasks;

namespace RegistrationStudent
{
    public class ConsumerStudent : IConsumer<IStudentCommand>
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public Task Consume(ConsumeContext<IStudentCommand> context)
        {
            var message = context.Message;
            logger.Info("Consume StudentCommandConsumer");
            logger.Info("Listen Data:{context}", context);
            Console.WriteLine($"Student successfully  registered. \n Student Name:{message.PersonDetails.Name} \n Student LastName:{message.PersonDetails.LastName}");
            //veri işlenir ve commandtypeın event tipinde event publish edilir.
            logger.Info("Veri işlendi ve event fırlatılacak!");
            context.Publish<IStudentEvent>(new
            {
                Message = "Başarı ile işlendi!",
                RegisterNumber = Guid.NewGuid()
            });
            return Task.CompletedTask;
        }
    }
}
