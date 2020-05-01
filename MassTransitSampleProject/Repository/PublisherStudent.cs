using MassTransit;
using MessageContracts;
using MessageContracts.Command;
using System;

namespace MassTransitSampleProject.Repository
{
    public class PublisherStudent
    {
        private readonly ISendEndpoint endPoint;

        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public PublisherStudent()
        {
            logger.Info("PublisherStudent dependency injection");
            var bus = BusConfigurator.ConfigureBus();
            var sendToUri = new Uri($"{RabbitMqConsts.RabbitMqUri}{RabbitMqConsts.StudentRegisterServiceQueue}");
            endPoint = bus.GetSendEndpoint(sendToUri).Result;
        }
        public string Publish(Models.Student student)
        {
            try
            {
                logger.Info("Publish Student Run");
                endPoint.Send<IStudentCommand>(new
                {
                    PersonDetails = student.PersonDetails,
                    ClassRoom = student.ClassRoom,
                    SchoolNumber = student.SchoolNumber
                });
                return "Başarı ile iletildi";//bunları messageresponse modeli oluşturarak yönetmelisiniz
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }
    }
}
