using System;

namespace MessageContracts.Event
{//MessageContract Kullanılacak katmanda Referans olarak eklemeyi unutmayın
    public interface IStudentEvent
    {
        public string Message { get; set; }
        public Guid RegisterNumber { get; set; }
    }
}
