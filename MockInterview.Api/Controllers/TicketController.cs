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
using Microsoft.AspNetCore.Identity;
using MockInterview.Api.Models.Users;

namespace MockInterview.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly ITicketService ticketService;
        private readonly IUserService userService;
        private readonly UserManager<IdentityUser> userManager;

        public TicketController(
            ITicketService ticketService,
            IUserService userService,
            UserManager<IdentityUser> userManager)
        {
            this.ticketService = ticketService;
            this.userService = userService;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("AddTicket")]
        public async Task<IActionResult> AddTicket([FromBody] CreateTicket ticket, string token)
        {
            var validation = this.userService.ValidateToken(token).Result;
            if (validation.Status == "Valid")
            {
                var user = userManager.FindByNameAsync(validation.Message);
                var role =  userManager.GetRolesAsync(user.Result);
                if(role.ToString()==UserRoles.Admin || role.ToString() == UserRoles.Interviewer)
                {
                    Ticket addedTicket = new Ticket
                    {
                        Id = new Guid(),
                        InterviewerId = user.Result.Id,
                        CreatedTime = DateTime.UtcNow,
                        StartTime = ticket.StartTime,
                        EndTime = ticket.EndTime,
                        Status = TicketStatus.Open,
                        Speciality = ticket.Speciality
                    };

                    await this.ticketService.AddTicketAsync(addedTicket);
                    return Ok(addedTicket);
                }
                else
                {
                    return Unauthorized();
                }
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

        [HttpGet("{ticketId}")]
        public async ValueTask<ActionResult<Ticket>> GetTicketByIdAsync(Guid ticketId)
        {
            Ticket ticket = await this.ticketService.RetrieveTicketByIdAsync(ticketId);
            return Ok(ticket);
        }

        [HttpPut]
        [Route("Update")]
        public async ValueTask<ActionResult<Ticket>> PutTicketAsync(Ticket ticket)
        {
            Ticket modifiedTicket = await this.ticketService.ModifyTicketAsync(ticket);
            return Ok(modifiedTicket);
        }

        [HttpDelete("{ticketId}")]
        public async ValueTask<ActionResult<Ticket>> DeleteTicketByIdAsync(Guid ticketId)
        {
            Ticket deletedTicket = await this.ticketService.RemovePostByIdAsync(ticketId);
            return Ok(deletedTicket);
        }

        [HttpGet]
        [Route("GetTicketsByOrder")]
        public ActionResult<IQueryable<Ticket>> GetTicketsByOrder(string sortOrder)
        {
            sortOrder = sortOrder == "ascending" ? "descending" : "ascending";
            IQueryable<Ticket> retrievedTickets = this.ticketService.RetrieveAllPosts();

            if (sortOrder == "ascending")
                retrievedTickets = retrievedTickets.OrderBy(ticket => ticket.StartTime);
            else
                retrievedTickets = retrievedTickets.OrderByDescending(ticket => ticket.StartTime);

            return Ok(retrievedTickets);
        }

        [HttpGet]
        [Route("GetTicketsByDate")]
        public ActionResult<IQueryable<Ticket>> GetTicketsByDate(DateTime dateTime)
        {
            IQueryable<Ticket> retrievedTickets = this.ticketService.RetrieveAllPosts();
            try
            {
                retrievedTickets = retrievedTickets.Where(ticket => ticket.StartTime == dateTime);

                return Ok(retrievedTickets);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
    }
}
