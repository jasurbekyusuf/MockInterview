// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace MockInterview.Api.Models.TicketEnrollments.Exceptions
{
	public class FailedTicketEnrollmentServiceException : Xeption
	{
		public FailedTicketEnrollmentServiceException(Exception innerException)
			: base(message: "Failed TicketEnrollment service occurred, please contact support.", innerException)
		{ }
	}
}
