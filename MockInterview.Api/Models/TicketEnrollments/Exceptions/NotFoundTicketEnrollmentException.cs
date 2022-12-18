//=================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to use to bring order in your workplace
//=================================

using System;
using Xeptions;

namespace MockInterview.Api.Models.TicketEnrollments.Exceptions
{
    public class NotFoundTicketEnrollmentException : Xeption
    {
        public NotFoundTicketEnrollmentException(Guid ticketId)
            : base(message:$"Couldn't find ticket with id: {ticketId}.")
        { }
    }
}
