//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to use to bring order in your workplace
//==================================================

using Microsoft.EntityFrameworkCore;
using MockInterview.Api.Models.Tickets;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MockInterview.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        DbSet<Ticket> Tickets { get; set; }

        public async ValueTask<Ticket> InsertTicketAsync(Ticket ticket) =>
            await InsertAsync(ticket);

        public IQueryable<Ticket> SelectAllTickets() =>
            SelectAll<Ticket>();

        public async ValueTask<Ticket> SelectTicketByIdAsync(Guid id) =>
            await SelectAsync<Ticket>(id);

        public async ValueTask<Ticket> UpdateTicketAsync(Ticket ticket) =>
            await UpdateAsync(ticket);

        public async ValueTask<Ticket> DeleteTicketAsync(Ticket ticket) =>
           await DeleteAsync(ticket);
    }
}
