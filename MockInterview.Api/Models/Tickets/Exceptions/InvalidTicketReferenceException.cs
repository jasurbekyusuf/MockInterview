using System;
using Xeptions;

namespace MockInterview.Api.Models.Tickets.Exceptions
{
    public class InvalidTicketReferenceException : Xeption
    {
        public InvalidTicketReferenceException(Exception innerException)
            : base(message: "Invalid ticket reference error occurred.", innerException)
        { }
    }
}
