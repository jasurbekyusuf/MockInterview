using MockInterview.Api.Models.TicketEnrollments;
using MockInterview.Api.Models.Tickets;
using System.Linq;
using System.Threading.Tasks;

namespace MockInterview.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<TicketEnrollment> InsertTicketEnrollmentAsync(TicketEnrollment ticketEnrollment);
        IQueryable<TicketEnrollment> SelectAllTicketEnrollments();
        ValueTask<TicketEnrollment> DeleteTicketEnrollmentAsync(TicketEnrollment ticketEnrollment);
    }
}
