using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using MockInterview.Api.Models.Tickets;
using MockInterview.Api.Services.Foundations.Tickets;
using MockInterview.Api.Models.Tickets.Exceptions;
using MockInterview.Api.Services.Users;
using System.Linq;
using Microsoft.Extensions.Hosting;

namespace MockInterview.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly ITicketService ticketService;
        private readonly UserService userService;

        public TicketController(
            ITicketService ticketService,
            UserService userService)
        {
            this.ticketService = ticketService;
            this.userService = userService;
        }

        [HttpPost]
        [Route("AddTicket")]
        public async Task<IActionResult> AddTicket([FromBody] Ticket ticket, string token)
        {
            var user = HttpContext.User;

            var validation = this.userService.ValidateToken(token).Result.Status;
            if (validation == "valid" && !user.IsInRole("User"))
            {
                Ticket addedTicket = await this.ticketService.AddTicketAsync(ticket);
                return Ok(addedTicket);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetAllTickets")]
        public ActionResult<IQueryable<Ticket>> GetAllTikcets()
        {
            IQueryable<Ticket> retrievedTickets = this.ticketService.RetrieveAllPosts();
            return Ok(retrievedTickets);

        }

        [HttpGet("{postId}")]
        public async ValueTask<ActionResult<Ticket>> GetTicketByIdAsync(Guid ticketId)
        {
            Ticket ticket = await this.ticketService.RetrieveTicketByIdAsync(ticketId);
            return Ok(ticket);
        }

        [HttpPut]
        public async ValueTask<ActionResult<Ticket>> PutTicketAsync(Ticket ticket)
        {
            Ticket modifiedTicket = await this.ticketService.ModifyTicketAsync(ticket);
            return Ok(modifiedTicket);
        }

        [HttpDelete("{postId}")]
        public async ValueTask<ActionResult<Ticket>> DeleteTicketByIdAsync(Guid ticketId)
        {
            Ticket deletedTicket = await this.ticketService.RemovePostByIdAsync(ticketId);
            return Ok(deletedTicket);
        }
    }
}
