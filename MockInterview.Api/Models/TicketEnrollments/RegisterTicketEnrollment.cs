using System;

namespace MockInterview.Api.Models.TicketEnrollments
{
    public class RegisterTicketEnrollment
    {
        public Guid TicketId { get; set; }
        public string Token { get; set; }
    }
}
