using System;
using Xeptions;

namespace MockInterview.Api.Models.Tickets.Exceptions
{
    public class FailedTicketServiceException : Xeption
    {
        public FailedTicketServiceException(Exception innerException)
            : base(message: "Failed ticket service occurred, please contact support", innerException)
        { }
    }
}
