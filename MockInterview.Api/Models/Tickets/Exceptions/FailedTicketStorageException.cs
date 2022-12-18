using System;
using Xeptions;

namespace MockInterview.Api.Models.Tickets.Exceptions
{
    public class FailedTicketStorageException : Xeption
    {
        public FailedTicketStorageException(Exception innerException)
            : base(message: "Failed ticket storage error occurred, contact support.", innerException)
        { }
    }
}
