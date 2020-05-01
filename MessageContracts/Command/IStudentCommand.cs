namespace MessageContracts.Command
{
    public interface IStudentCommand
    {//MessageContract Kullanılacak katmanda Referans olarak eklemeyi unutmayın
        public IPersonCommand PersonDetails { get; set; }
        public string ClassRoom { get; set; }
        public int SchoolNumber { get; set; }
    }
}
