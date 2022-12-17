//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to use to bring order in your workplace
//==================================================

using MockInterview.Api.Models.Tickets;
using System.Linq;
using System.Threading.Tasks;

namespace MockInterview.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Ticket> InsertTicketAsync(Ticket ticket);
        IQueryable<Ticket> SelectAllTickets();
        ValueTask<Ticket> UpdateTicketAsync(Ticket student);
    }
}
