// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace MockInterview.Api.Models.TicketEnrollments.Exceptions
{
	public class InvalidTicketEnrollmentReferenceException : Xeption
	{
		public InvalidTicketEnrollmentReferenceException(Exception innerException)
			: base(message: "Invalid TicketEnrollment reference error occurred.", innerException)
		{ }
	}
}

