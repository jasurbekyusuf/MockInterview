// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EFxceptions.Models.Exceptions;
using MockInterview.Api.Models.TicketEnrollments;
using MockInterview.Api.Models.TicketEnrollments.Exceptions;
using Xeptions;

namespace MockInterview.Api.Services.Foundations
{
    public partial class TicketEnrollmentService
    {
        private delegate ValueTask<TicketEnrollment> ReturningTicketEnrollmentFunction();
        private delegate IQueryable<TicketEnrollment> ReturningTicketEnrollmentsFunction();

        private async ValueTask<TicketEnrollment> TryCatch(ReturningTicketEnrollmentFunction returningTicketEnrollmentFunction)
        {
            try
            {
                return await returningTicketEnrollmentFunction();
            }
            catch (NullTicketEnrollmentException nullTicketEnrollmentException)
            {
                throw CreateAndLogValidationException(nullTicketEnrollmentException);
            }
            catch (InvalidTicketEnrollmentException invalidTicketEnrollmentException)
            {
                throw CreateAndLogValidationException(invalidTicketEnrollmentException);
            }
           
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsTicketEnrollmentException =
                    new AlreadyExistsTicketEnrollmentException(duplicateKeyException);

                throw CreateAndLogDependencyValidationException(alreadyExistsTicketEnrollmentException);
            }
           
            catch (ForeignKeyConstraintConflictException foreignKeyConstraintConflictException)
            {
                var invalidTicketEnrollmentReferenceException =
                    new InvalidTicketEnrollmentReferenceException(foreignKeyConstraintConflictException);

                throw CreateAndLogDependencyValidationException(invalidTicketEnrollmentReferenceException);
            }
            catch (Exception exception)
            {
                var failedTicketEnrollmentServiceException =
                    new FailedTicketEnrollmentServiceException(exception);

                throw CreateAndLogServiceException(failedTicketEnrollmentServiceException);
            }
        }

        private IQueryable<TicketEnrollment> TryCatch(ReturningTicketEnrollmentsFunction returningPostsImpressionsFunction)
        {
            try
            {
                return returningPostsImpressionsFunction();
            }
            
            catch (Exception serviceException)
            {
                var failedTicketEnrollmentServiceException =
                    new FailedTicketEnrollmentServiceException(serviceException);

                throw CreateAndLogServiceException(failedTicketEnrollmentServiceException);
            }
        }

        private TicketEnrollmentValidationException CreateAndLogValidationException(Xeption exception)
        {
            var TicketEnrollmentValidationException =
                new TicketEnrollmentValidationException(exception);

            this.loggingBroker.LogError(TicketEnrollmentValidationException);

            return TicketEnrollmentValidationException;
        }

        private TicketEnrollmentDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var TicketEnrollmentDependencyException = new TicketEnrollmentDependencyException(exception);
            this.loggingBroker.LogCritical(TicketEnrollmentDependencyException);

            return TicketEnrollmentDependencyException;
        }

        private TicketEnrollmentDependencyValidationException CreateAndLogDependencyValidationException(
            Xeption exception)
        {
            var TicketEnrollmentDependencyValidationException =
                new TicketEnrollmentDependencyValidationException(exception);

            this.loggingBroker.LogError(TicketEnrollmentDependencyValidationException);

            return TicketEnrollmentDependencyValidationException;
        }

        private TicketEnrollmentDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var TicketEnrollmentDependencyException = new TicketEnrollmentDependencyException(exception);
            this.loggingBroker.LogError(TicketEnrollmentDependencyException);

            return TicketEnrollmentDependencyException;
        }

        private TicketEnrollmentServiceException CreateAndLogServiceException(Exception exception)
        {
            var TicketEnrollmentServiceException = new TicketEnrollmentServiceException(exception);
            this.loggingBroker.LogError(TicketEnrollmentServiceException);

            return TicketEnrollmentServiceException;
        }
    }
}

