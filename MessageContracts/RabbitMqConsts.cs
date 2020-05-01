namespace MessageContracts
{
    public static class RabbitMqConsts
    {
        //ilgili tanımlamalar
        public const string RabbitMqUri = "rabbitmq://localhost/";//sunucu adresi
        public const string UserName = "guest";//username
        public const string Password = "guest";//pass

        //kuyuruk exchange tanımlamalarıdır
        public const string StudentRegisterServiceQueue = "StudentRegister.service";
        public const string StudentNotificationServiceQueue = "StudentNotification.service";
        public const string StudentThirdPartyServiceQueue = "StudentThirdparty.service";
    }
}
