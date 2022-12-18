using MockInterview.Api.Models.Tickets.Exceptions;
using MockInterview.Api.Models.Tickets;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MockInterview.Api.Tests.Unit.Services.Foundations.Tickets
{
    public partial class TicketServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfProfileIsNullAndLogItAsync()
        {
            // given
            Ticket invalidTicket = null;

            var nullTicketException =
                new NullTicketException();

            var expectedTicketValidationException =
                new TicketValidationException(nullTicketException);

            // when
            ValueTask<Ticket> addTicketTask =
                this.ticketService.AddTicketAsync(invalidTicket);

            // then
            await Assert.ThrowsAsync<TicketValidationException>(() =>
                addTicketTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedTicketValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertTicketAsync(invalidTicket),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
