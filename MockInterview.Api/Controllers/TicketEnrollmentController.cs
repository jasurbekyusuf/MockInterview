//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to use to bring order in your workplace
//==================================================

using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using MockInterview.Api.Services.Foundations;
using MockInterview.Api.Models.TicketEnrollments;
using MockInterview.Api.Models.TicketEnrollments.Exceptions;

namespace MockInterview.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketEnrollmentController : RESTFulController
    {
        private readonly ITicketEnrollmentService ticketEnrollmentService;

        public TicketEnrollmentController(ITicketEnrollmentService ticketEnrollmentService) =>
            this.ticketEnrollmentService = ticketEnrollmentService;

        [HttpPost]
        public async ValueTask<ActionResult<TicketEnrollment>> PostTicketAsync(TicketEnrollment ticketEnrollment)
        {
            try
            {
                return await this.ticketEnrollmentService.AddTicketEnrollmentAsync(ticketEnrollment);
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
