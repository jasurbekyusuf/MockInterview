// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Microsoft.Data.SqlClient;
using MockInterview.Api.Brokers.Loggings;
using MockInterview.Api.Brokers.Storages;
using MockInterview.Api.Models.Tickets;
using MockInterview.Api.Services.Foundations;
using MockInterview.Api.Services.Foundations.TicketEnrollments;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace MockInterview.Api.Tests.Unit.Services.Foundations.TicketEnrollment
{
    public partial class TicketEnrollmentServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ITicketEnrollmentService ticketEnrollmentService;

        public TicketEnrollmentServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.ticketEnrollmentService = new TicketEnrollmentService(
                storageBroker: this.storageBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        public static TheoryData<int> InvalidSeconds()
        {
            int secondsInPast = -1 * new IntRange(
                min: 60,
                max: short.MaxValue).GetValue();

            int secondsInFuture = new IntRange(
                min: 0,
                max: short.MaxValue).GetValue();

            return new TheoryData<int>
            {
                secondsInPast,
                secondsInFuture
            };
        }

        private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: DateTime.UnixEpoch).GetValue();

        private static string GetRandomMessage() =>
            new MnemonicString(wordCount: GetRandomNumber()).GetValue();

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static SqlException CreateSqlException() =>
            (SqlException)FormatterServices.GetUninitializedObject(typeof(SqlException));

        private static IQueryable<Ticket> CreateRandomTickets()
        {
            return CreateTicketFiller(dates: GetRandomDateTime())
                .Create(count: GetRandomNumber()).AsQueryable();
        }

        private static Ticket CreateRandomTicket(DateTimeOffset dates) =>
            CreateTicketFiller(dates).Create();

        private static T GetInvalidEnum<T>()
        {
            int randomNumber = GetRandomNumber();

            while (Enum.IsDefined(typeof(T), randomNumber))
            {
                randomNumber = GetRandomNumber();
            }

            return (T)(object)randomNumber;
        }

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 99).GetValue();

        private static Ticket CreateRandomTicket() =>
            CreateTicketFiller(GetRandomDateTime()).Create();

        private static Filler<Ticket> CreateTicketFiller(DateTimeOffset dates)
        {
            var filler = new Filler<Ticket>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(dates);

            return filler;
        }
    }
}
