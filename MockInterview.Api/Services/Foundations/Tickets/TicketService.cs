using MockInterview.Api.Brokers.Loggings;
using MockInterview.Api.Brokers.Storages;
using System.Threading.Tasks;
using MockInterview.Api.Models.Tickets;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace MockInterview.Api.Services.Foundations.Tickets
{
    public partial class TicketService : ITicketService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public TicketService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Ticket> AddTicketAsync(Ticket ticket) =>
            TryCatch(async () =>
            {
                ValidateTicketOnAdd(ticket);

                return await this.storageBroker.InsertTicketAsync(ticket);
            });

        public IQueryable<Ticket> RetrieveAllPosts() =>
        TryCatch(() => this.storageBroker.SelectAllTickets());

        public ValueTask<Ticket> RetrieveTicketByIdAsync(Guid ticketId) =>
        TryCatch(async () =>
        {
            ValidateTicketId(ticketId);

            Ticket maybeTicket = await this.storageBroker
                .SelectTicketByIdAsync(ticketId);

            ValidateStorageTicket(maybeTicket, ticketId);

            return maybeTicket;
        });

        public ValueTask<Ticket> ModifyTicketAsync(Ticket ticket) =>
        TryCatch(async () =>
        {
            ValidatePostOnModify(ticket);

            Ticket maybeTicket =
                await this.storageBroker.SelectTicketByIdAsync(ticket.Id);

            ValidateStorageTicket(maybeTicket, ticket.Id);

            return await this.storageBroker.UpdateTicketAsync(ticket);
        });

        public ValueTask<Ticket> RemovePostByIdAsync(Guid ticketId) =>
        TryCatch(async () =>
        {
            ValidateTicketId(ticketId);

            Ticket maybeTicket = await this.storageBroker
                .SelectTicketByIdAsync(ticketId);

            ValidateStorageTicket(maybeTicket, ticketId);

            return await this.storageBroker
                .DeleteTicketAsync(maybeTicket);
        });
    }
}
