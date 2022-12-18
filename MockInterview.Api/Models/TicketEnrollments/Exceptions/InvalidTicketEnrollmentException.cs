// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace MockInterview.Api.Models.TicketEnrollments.Exceptions
{
	public class InvalidTicketEnrollmentException : Xeption
	{
		public InvalidTicketEnrollmentException()
			: base(message: "Invalid TicketEnrollment. Please correct the errors and try again.")
		{ }
	}
}
