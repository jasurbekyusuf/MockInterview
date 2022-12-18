using Xeptions;

namespace MockInterview.Api.Models.Tickets.Exceptions
{
    public class TicketDependencyException : Xeption
    {
        public TicketDependencyException(Xeption innerException)
            : base(message: "Ticket dependency error occurred, contact support.", innerException)
        { }
    }
}