using MockInterview.Api.Models.TicketEnrollments;
using MockInterview.Api.Models.Tickets;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MockInterview.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<TicketEnrollment> InsertTicketEnrollmentAsync(TicketEnrollment ticketEnrollment);
        IQueryable<TicketEnrollment> SelectAllTicketEnrollments();
        ValueTask<TicketEnrollment> UpdateTicketEnrollmentAsync(TicketEnrollment ticketEnrollment);
        ValueTask<TicketEnrollment> SelectTicketEnrollmentByIdAsync(Guid id);
        ValueTask<TicketEnrollment> DeleteTicketEnrollmentAsync(TicketEnrollment ticketEnrollment);
    }
}
