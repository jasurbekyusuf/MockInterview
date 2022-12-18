// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using MockInterview.Api.Brokers.Loggings;
using MockInterview.Api.Brokers.Storages;
using MockInterview.Api.Models.TicketEnrollments;

namespace MockInterview.Api.Services.Foundations
{
    public partial class TicketEnrollmentService : ITicketEnrollmentService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public TicketEnrollmentService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<TicketEnrollment> AddTicketEnrollmentAsync(TicketEnrollment ticketEnrollment) =>
        TryCatch(async () =>
        {
            ValidateTicketEnrollment(ticketEnrollment);

            return await this.storageBroker.InsertTicketEnrollmentAsync(ticketEnrollment);
        });

        public IQueryable<TicketEnrollment> RetrieveAllTicketEnrollments() =>
        TryCatch(() => this.storageBroker.SelectAllTicketEnrollments());

        public ValueTask<TicketEnrollment> RetrieveTicketEnrolmentByIdAsync(Guid ticketEnrollmentId) =>
        TryCatch(async () =>
        {
            ValidateTicketEnrollmentId(ticketEnrollmentId);

            TicketEnrollment maybeTicketEnrollment =
                await this.storageBroker.SelectTicketEnrollmentByIdAsync(ticketEnrollmentId);

            ValidateStorageTicketEnrollment(maybeTicketEnrollment, ticketEnrollmentId);

            return maybeTicketEnrollment;
        });
    }
}