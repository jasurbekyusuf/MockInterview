using MockInterview.Api.Models.Tickets;
using System;

namespace MockInterview.Api.Models.TicketEnrollments
{
    public class TicketEnrollment
    {
        public Guid Id { get; set; }

        public Ticket Ticket { get; set; }

        public string CandidateId { get; set; }

        public DateTime EnrollmentTime { get; set; }
    }
}
