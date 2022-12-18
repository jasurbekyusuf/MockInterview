using Xeptions;

namespace MockInterview.Api.Models.Tickets.Exceptions
{
    public class InvalidTicketException : Xeption
    {
        public InvalidTicketException()
            : base(message: "Invalid ticket. Please correct the errors and try again.")
        { }
    }
}
