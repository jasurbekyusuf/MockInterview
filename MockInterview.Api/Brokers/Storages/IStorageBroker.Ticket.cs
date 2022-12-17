using MockInterview.Api.Models.Tickets;
using System.Threading.Tasks;

namespace MockInterview.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Ticket> InsertTicketAsync(Ticket ticket);
    }
}
