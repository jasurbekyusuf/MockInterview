using System;
using Xeptions;

namespace MockInterview.Api.Models.Tickets.Exceptions
{
    public class AlreadyExistsTicketException : Xeption
    {
        public AlreadyExistsTicketException(Exception innerException)
           : base(message: "Ticket with the same id already exists.", innerException)
        { }
    }
}
