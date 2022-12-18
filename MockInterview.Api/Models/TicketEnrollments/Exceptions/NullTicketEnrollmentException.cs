//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to use to bring order in your workplace
//==================================================

using Xeptions;

namespace MockInterview.Api.Models.TicketEnrollments.Exceptions
{
    public class NullTicketEnrollmentException : Xeption
    {
        public NullTicketEnrollmentException()
            : base(message: "Ticket Enrollment is null.")
        { }
    }
}
