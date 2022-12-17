using Microsoft.EntityFrameworkCore;
using MockInterview.Api.Models.Tickets;

namespace MockInterview.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        DbSet<Ticket> Tickets { get; set; }
    }
}
