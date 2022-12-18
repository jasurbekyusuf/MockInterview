// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace MockInterview.Api.Models.TicketEnrollments.Exceptions
{
	public class TicketEnrollmentDependencyException : Xeption
	{
		public TicketEnrollmentDependencyException(Xeption innerException)
			: base(message: "Post impression dependency error has occurred, please contact support.", innerException)
		{ }
	}
}
