using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using Xeptions;
using MockInterview.Api.Models.Tickets;
using MockInterview.Api.Models.Tickets.Exceptions;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace MockInterview.Api.Services.Foundations.Tickets
{
    public partial class TicketService
    {
        private delegate ValueTask<Ticket> ReturningTicketsFunction();
        private delegate IQueryable<Ticket> ReturningTicketFunction();

        private async ValueTask<Ticket> TryCatch(ReturningTicketsFunction returningTicketFunction)
        {
            try
            {
                return await returningTicketFunction();
            }
            catch (NullTicketException nullTicketException)
            {
                throw CreateAndLogValidationException(nullTicketException);
            }
            catch (SqlException sqlException)
            {
                var failedTicketStorageException =
                    new FailedTicketStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(failedTicketStorageException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistTicketException =
                    new AlreadyExistsTicketException(duplicateKeyException);

                throw CreateAndLogDependencyValidationException(alreadyExistTicketException);
            }
            catch (ForeignKeyConstraintConflictException foreignKeyConstraintConflictException)
            {
                var invalidTicketReferenceException =
                    new InvalidTicketReferenceException(foreignKeyConstraintConflictException);

                throw CreateAndLogDependencyValidationException(invalidTicketReferenceException);
            }
            catch (DbUpdateException databaseUpdateException)
            {
                var failedStorageTicketException =
                    new FailedTicketStorageException(databaseUpdateException);

                throw CreateAndLogDependencyException(failedStorageTicketException);
            }
            catch (InvalidTicketException invalidTicketException)
            {
                throw CreateAndLogValidationException(invalidTicketException);
            }
            catch (Exception serviceException)
            {
                var failedServiceTicketException =
                    new FailedTicketServiceException(serviceException);

                throw CreateAndLogServiceException(failedServiceTicketException);
            }
        }

        private IQueryable<Ticket> TryCatch(ReturningTicketFunction returningPostsFunction)
        {
            try
            {
                return returningPostsFunction();
            }
            catch (SqlException sqlException)
            {
                var failedPostStorageException =
                    new FailedTicketStorageException(sqlException);
                throw CreateAndLogCriticalDependencyException(failedPostStorageException);
            }
            catch (Exception exception)
            {
                var failedPostServiceException =
                    new FailedTicketServiceException(exception);

                throw CreateAndLogServiceException(failedPostServiceException);
            }
        }

        private TicketValidationException CreateAndLogValidationException(Xeption exception)
        {
            var ticketValidationException =
                new TicketValidationException(exception);

            this.loggingBroker.LogError(ticketValidationException);

            return ticketValidationException;
        }

        private TicketDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var ticketDependencyException =
                new TicketDependencyException(exception);

            this.loggingBroker.LogCritical(ticketDependencyException);

            return ticketDependencyException;
        }

        private TicketDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var ticketDependencyException =
                new TicketDependencyException(exception);

            this.loggingBroker.LogError(ticketDependencyException);

            return ticketDependencyException;
        }

        private TicketDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var ticketDependencyValidationException =
                new TicketDependencyValidationException(exception);

            this.loggingBroker.LogError(ticketDependencyValidationException);

            return ticketDependencyValidationException;
        }

        private TicketServiceException CreateAndLogServiceException(Xeption exception)
        {
            var ticketServiceException =
                new TicketServiceException(exception);

            this.loggingBroker.LogError(ticketServiceException);

            return ticketServiceException;
        }
    }
}
