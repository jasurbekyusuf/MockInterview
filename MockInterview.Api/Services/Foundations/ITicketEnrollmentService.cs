// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using MockInterview.Api.Models.TicketEnrollments;

namespace MockInterview.Api.Services.Foundations
{
    public interface ITicketEnrollmentService
    {
        ValueTask<TicketEnrollment> AddTicketEnrollmentAsync(TicketEnrollment ticketEnrolment);
        IQueryable<TicketEnrollment> RetrieveAllTicketEnrollments();
        ValueTask<TicketEnrollment> RetrieveTicketEnrolmentByIdAsync(Guid ticketEnrollmentId);

    }
}
