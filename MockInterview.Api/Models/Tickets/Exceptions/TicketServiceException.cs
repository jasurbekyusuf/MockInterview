using System;
using Xeptions;

namespace MockInterview.Api.Models.Tickets.Exceptions
{
    public class TicketServiceException : Xeption
    {
        public TicketServiceException(Exception innerException)
            : base(message: "Ticket service error occurred, contact support.", innerException)
        { }
    }
}
