using Microsoft.EntityFrameworkCore;
using MockInterview.Api.Models.Tickets;
using System.Net.Sockets;

namespace MockInterview.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        DbSet<Ticket> Tickets { get; set; }
    }
}
