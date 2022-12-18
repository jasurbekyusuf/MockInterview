using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Threading.Tasks;
using MockInterview.Api.Models.Tickets;
using System;
using System.Linq;

namespace MockInterview.Api.Services.Foundations.Tickets
{
    public partial interface ITicketService
    {
        ValueTask<Ticket> AddTicketAsync(Ticket ticket);
        ValueTask<Ticket> RemovePostByIdAsync(Guid ticketId);
        ValueTask<Ticket> ModifyTicketAsync(Ticket ticket);
        ValueTask<Ticket> RetrieveTicketByIdAsync(Guid ticketId);
        IQueryable<Ticket> RetrieveAllPosts();
    }
}
