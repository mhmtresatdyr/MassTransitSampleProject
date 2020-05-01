namespace MessageContracts.Command
{
    //MessageContract Kullanılacak katmanda Referans olarak eklemeyi unutmayın
    public interface IPersonCommand
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
    }
}
