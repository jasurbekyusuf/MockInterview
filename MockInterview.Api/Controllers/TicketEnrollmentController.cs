//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to use to bring order in your workplace
//==================================================

using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using MockInterview.Api.Models.TicketEnrollments;
using MockInterview.Api.Models.TicketEnrollments.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MockInterview.Api.Models.Tickets;
using MockInterview.Api.Services.Foundations.Tickets;
using MockInterview.Api.Services.Users;
using MockInterview.Api.Services.Foundations.TicketEnrollments;
using Microsoft.AspNetCore.Authentication;

namespace MockInterview.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketEnrollmentController : RESTFulController
    {
        private readonly IConfiguration configuration;
        private readonly ITicketEnrollmentService ticketEnrollmentService;
        private readonly IUserService userService;

        public TicketEnrollmentController(
            ITicketEnrollmentService ticketEnrollmentService,
            IUserService userService)
        {
            this.ticketEnrollmentService = ticketEnrollmentService;
            this.userService = userService;
        }

        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        [HttpPost]
        [Route("AddTicketEnrollment")]
        public async ValueTask<ActionResult<TicketEnrollment>> PostTicketEnrollmentAsync([FromBody]TicketEnrollment ticketEnrollment, string token)
        {
            try
            {

                var user = HttpContext.User;

                var validation = this.userService.ValidateToken(token).Result.Status;
                if (validation == "Valid")
                {
                    //await HttpContext.SignInAsync(validation, this.userService.GetClaim(token));
                    
                    var res = await this.ticketEnrollmentService.AddTicketEnrollmentAsync(ticketEnrollment);
                    return Ok(res);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (TicketEnrollmentValidationException ticketEnrollmentValidationException)
            {
                return BadRequest(ticketEnrollmentValidationException.InnerException);
            }
            catch (TicketEnrollmentDependencyValidationException ticketDependencyValidationException)
                when (ticketDependencyValidationException.InnerException is AlreadyExistsTicketEnrollmentException)
            {
                return Conflict(ticketDependencyValidationException.InnerException);
            }
            catch (TicketEnrollmentDependencyValidationException ticketDependencyValidationException)
            {
                return BadRequest(ticketDependencyValidationException.InnerException);
            }
            catch (TicketEnrollmentDependencyException ticketDependencyException)
            {
                return InternalServerError(ticketDependencyException.InnerException);
            }
            catch (TicketEnrollmentServiceException ticketServiceException)
            {
                return InternalServerError(ticketServiceException.InnerException);
            }
        }

        [HttpGet]
        public async ValueTask<ActionResult<TicketEnrollment>> GetTicketByIdAsync(Guid id)
        {
            try
            {
                return await this.ticketEnrollmentService.RetrieveTicketEnrolmentByIdAsync(id);
            }
            catch (TicketEnrollmentDependencyException ticketDependencyException)
            {
                return InternalServerError(ticketDependencyException.InnerException);
            }
            catch (TicketEnrollmentDependencyValidationException ticketValidationException)
                when (ticketValidationException.InnerException is InvalidTicketEnrollmentException)
            {
                return BadRequest(ticketValidationException.InnerException);
            }
            catch (TicketEnrollmentValidationException ticketValidationException)
                when (ticketValidationException.InnerException is NotFoundTicketEnrollmentException)
            {
                return NotFound(ticketValidationException.InnerException);
            }
            catch (TicketEnrollmentServiceException ticketServiceException)
            {
                return InternalServerError(ticketServiceException.InnerException);
            }
        }
    }
}
