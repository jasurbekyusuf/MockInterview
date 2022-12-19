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
using System.Linq;

namespace MockInterview.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketEnrollmentController : RESTFulController
    {
        private readonly ITicketEnrollmentService ticketEnrollmentService;
        private readonly IUserService userService;
        private readonly ITicketService ticketService;
        private readonly UserManager<IdentityUser> userManager;

        public TicketEnrollmentController(
            ITicketEnrollmentService ticketEnrollmentService,
            IUserService userService,
            ITicketService ticketService,
            UserManager<IdentityUser> userManager)
        {
            this.ticketEnrollmentService = ticketEnrollmentService;
            this.userService = userService;
            this.ticketService = ticketService;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("AddTicketEnrollment")]
        public async ValueTask<ActionResult<TicketEnrollment>> PostTicketEnrollmentAsync([FromBody]RegisterTicketEnrollment registerTicketEnrollment)
        {
            try
            {
                var validation = this.userService.ValidateToken(registerTicketEnrollment.Token).Result;
                var user = userManager.FindByNameAsync(validation.Message).Result;
                if (validation.Status == "Valid")
                {
                    //await HttpContext.SignInAsync(validation, this.userService.GetClaim(token));
                    
                    var ticket = await this.ticketService.RetrieveTicketByIdAsync(registerTicketEnrollment.TicketId);
                    var ticketEnrollment = new TicketEnrollment
                    {
                        Id = new Guid(),
                        Ticket = ticket,
                        EnrollmentTime = DateTime.UtcNow,
                        CandidateId = user.Id,
                    };
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
        [Route("GetTicketEnrollmentById")]
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

        [HttpGet]
        [Route("GetAllTicketEnrollments")]
        public ActionResult<IQueryable<TicketEnrollment>> GetAllTicketEnrollments()
        {
            IQueryable<TicketEnrollment> retrievedTicketEnrollments = this.ticketEnrollmentService.RetrieveAllTicketEnrollments();
            return Ok(retrievedTicketEnrollments);
        }
    }
}
