using System;

namespace MockInterview.Api.Models.Tickets
{
    public class CreateTicket
    {

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Speciality { get; set; }
    }
}
