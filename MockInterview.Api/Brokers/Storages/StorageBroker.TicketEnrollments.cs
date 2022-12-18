using Microsoft.EntityFrameworkCore;
using MockInterview.Api.Models.TicketEnrollments;
using MockInterview.Api.Models.Tickets;
using System.Linq;
using System.Threading.Tasks;

namespace MockInterview.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        DbSet<TicketEnrollment> TicketEnrollments { get; set; }

        public async ValueTask<TicketEnrollment> InsertTicketEnrollmentAsync(TicketEnrollment ticketEnrollment) =>
            await InsertAsync(ticketEnrollment);

        public IQueryable<TicketEnrollment> SelectAllTicketEnrollments() =>
            SelectAll<TicketEnrollment>();

        public async ValueTask<TicketEnrollment> DeleteTicketEnrollmentAsync(TicketEnrollment ticketEnrollment) =>
           await DeleteAsync(ticketEnrollment);

        public async ValueTask<TicketEnrollment> UpdateTicketEnrollmentAsync(TicketEnrollment ticketEnrollment) =>
            await UpdateAsync(ticketEnrollment);
    }
}
