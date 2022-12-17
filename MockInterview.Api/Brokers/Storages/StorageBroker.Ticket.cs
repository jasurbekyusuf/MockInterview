using Microsoft.EntityFrameworkCore;
using MockInterview.Api.Models.Tickets;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace MockInterview.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        DbSet<Ticket> Tickets { get; set; }

        public async ValueTask<Ticket> InsertPostAsync(Ticket ticket) =>
            await InsertAsync(ticket);

        public IQueryable<Ticket> SelectAllTickets() =>
            SelectAll<Ticket>();
    }
}
