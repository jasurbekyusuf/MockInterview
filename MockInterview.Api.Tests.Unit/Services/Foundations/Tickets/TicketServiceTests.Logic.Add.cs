using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using MockInterview.Api.Models.Tickets;
using Force.DeepCloner;
using FluentAssertions;

namespace MockInterview.Api.Tests.Unit.Services.Foundations.Tickets
{
    public partial class TicketServiceTests
    {
        [Fact]
        public async Task ShoudAddTicketAsync()
        {
            // given
            Ticket randomTicket = CreateRandomTicket();
            Ticket inputTicket = randomTicket;
            Ticket persistedTicket = inputTicket;
            Ticket expectedTicket = persistedTicket.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertTicketAsync(inputTicket))
                    .ReturnsAsync(persistedTicket);

            // Wen
            Ticket actualTicket = await this.ticketService.AddTicketAsync(inputTicket);

            // then
            actualTicket.Should().BeEquivalentTo(expectedTicket);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertTicketAsync(inputTicket), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
