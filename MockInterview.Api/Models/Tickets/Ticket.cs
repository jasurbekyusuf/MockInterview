using System;

namespace MockInterview.Api.Models.Tickets
{
    public class Ticket
    {
        public Guid Id { get; set; }

        public Guid InterviewerId { get; set; }

        public DateTime CreatedTime { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public TicketStatus Status { get; set; }

        public string Speciality { get; set; }
    }
}
