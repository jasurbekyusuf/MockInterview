using Xeptions;

namespace MockInterview.Api.Models.Tickets.Exceptions
{
    public class TicketValidationException : Xeption
    {
        public TicketValidationException(Xeption innerException)
            : base(message: "Ticket validation errors occurred, please try again.",
                  innerException)
        { }
    }
}
