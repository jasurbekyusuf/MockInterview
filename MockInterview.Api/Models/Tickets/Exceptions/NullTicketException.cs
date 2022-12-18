using Xeptions;

namespace MockInterview.Api.Models.Tickets.Exceptions
{
    public class NullTicketException : Xeption
    {
        public NullTicketException()
            : base(message: "Ticket is null.")
        { }
    }
}
