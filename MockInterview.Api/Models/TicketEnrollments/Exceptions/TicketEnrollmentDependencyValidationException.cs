//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to use to bring order in your workplace
//==================================================

using Xeptions;

namespace MockInterview.Api.Models.TicketEnrollments.Exceptions
{
    public class TicketEnrollmentDependencyValidationException : Xeption
    {
        public TicketEnrollmentDependencyValidationException(Xeption innerException)
            : base(message: "Ticket Enrollments dependency validation occurred, please try again.", innerException)
        { }
    }
}
