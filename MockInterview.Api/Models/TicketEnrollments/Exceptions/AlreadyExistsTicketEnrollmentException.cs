//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to use to bring order in your workplace
//==================================================

using System;
using Xeptions;

namespace MockInterview.Api.Models.TicketEnrollments.Exceptions
{
    public class AlreadyExistsTicketEnrollmentException : Xeption
    {
        public AlreadyExistsTicketEnrollmentException(Exception innerException)
            : base(message: "TicketEnrollments with the same id already exists.", innerException)
        { }
    }
}
