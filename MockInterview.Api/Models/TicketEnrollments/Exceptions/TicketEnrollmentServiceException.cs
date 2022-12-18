//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to use to bring order in your workplace
//==================================================

using System;
using Xeptions;

namespace MockInterview.Api.Models.TicketEnrollments.Exceptions
{
    public class TicketEnrollmentServiceException : Xeption
    {
        public TicketEnrollmentServiceException(Exception innerException)
            : base(message: "TicketEnrollment service error occurred, please contact support.", innerException)
        { }
    }
}
