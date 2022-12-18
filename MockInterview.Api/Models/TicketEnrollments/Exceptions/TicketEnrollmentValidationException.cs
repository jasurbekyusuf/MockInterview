//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to use to bring order in your workplace
//==================================================

using Xeptions;

namespace MockInterview.Api.Models.TicketEnrollments.Exceptions
{
    public class TicketEnrollmentValidationException : Xeption
    {
        public TicketEnrollmentValidationException(Xeption innerException)
            : base(message: "TicketEnrollment validation errors occurred, please try again.",
                  innerException)
        { }
    }
}
