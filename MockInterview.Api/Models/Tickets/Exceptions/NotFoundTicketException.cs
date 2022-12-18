using System;
using Xeptions;

namespace MockInterview.Api.Models.Tickets.Exceptions
{
    public class NotFoundTicketException : Xeption
    {
        public NotFoundTicketException(Guid postId)
            : base(message: $"Couldn't find ticket with id: {postId}.")
        { }
    }
}
