using Xeptions;

namespace MockInterview.Api.Models.Tickets.Exceptions
{
    public class TicketDependencyValidationException : Xeption
    {
        public TicketDependencyValidationException(Xeption innerException)
            : base(message: "Ticket dependency validation occurred, please try again.", innerException)
        { }
    }
}
