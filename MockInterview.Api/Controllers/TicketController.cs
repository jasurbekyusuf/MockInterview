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

namespace MockInterview.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        //private readonly IConfiguration configuration;
        //private readonly ITicketService ticketService;

        //public TicketController(ITicketService ticketService) =>
        //    this.ticketService = ticketService;

        //private bool TokenGeneration()
        //{
        //    return true;
        //}

        //[HttpPost]
        //public async ValueTask<ActionResult<Ticket>> PostTicketAsync(Ticket ticket)
        //{
        //    try
        //    {
        //        Ticket addedTicket =
        //            await this.ticketService.AddTicketAsync(ticket);

        //        return Created(addedTicket);
        //    }
        //    catch (TicketValidationException postValidationException)
        //    {
        //        return BadRequest(postValidationException.InnerException);
        //    }
        //    catch (TicketDependencyValidationException postDependencyValidationException)
        //       when (postDependencyValidationException.InnerException is AlreadyExistsTicketException)
        //    {
        //        return Conflict(postDependencyValidationException.InnerException);
        //    }
        //    catch (TicketDependencyException postDependencyException)
        //    {
        //        return InternalServerError(postDependencyException);
        //    }
        //    catch (TicketServiceException postServiceException)
        //    {
        //        return InternalServerError(postServiceException);
        //    }
        //}


        //private JwtSecurityToken GetToken(List<Claim> authClaims)
        //{
        //    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

        //    var token = new JwtSecurityToken(
        //        issuer: configuration["JWT:ValidIssuer"],
        //        audience: configuration["JWT:ValidAudience"],
        //        expires: DateTime.Now.AddHours(3),
        //        claims: authClaims,
        //        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        //        );

        //    return token;
        //}
    }
}
