// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using MockInterview.Api.Models.TicketEnrollments;
using MockInterview.Api.Models.TicketEnrollments.Exceptions;

namespace MockInterview.Api.Services.Foundations
{
    public partial class TicketEnrollmentService
    {
        private void ValidateTicketEnrollment(TicketEnrollment TicketEnrollment)
        {
            ValidateTicketEnrollmentNotNull(TicketEnrollment);

            Validate(
                (Rule: IsInvalid(TicketEnrollment.Id), Parameter: nameof(TicketEnrollment.Id)),
                (Rule: IsInvalid(TicketEnrollment.Ticket), Parameter: nameof(TicketEnrollment.Ticket)),
                (Rule: IsInvalid(TicketEnrollment.CandidateId), Parameter: nameof(TicketEnrollment.CandidateId)),
                (Rule: IsInvalid(TicketEnrollment.EnrollmentTime), Parameter: nameof(TicketEnrollment.EnrollmentTime)));
        }

        private void ValidateTicketEnrollmentId(Guid TicketEnrollmentId) =>
            Validate((Rule: IsInvalid(TicketEnrollmentId), Parameter: nameof(TicketEnrollment.Id)));

        private void ValidateStorageTicketEnrollment(TicketEnrollment maybeTicketEnrollment, Guid ticketEnrollmentId)
        {
            if (maybeTicketEnrollment is null)
            {
                throw new NotFoundTicketEnrollmentException(ticketEnrollmentId);
            }
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == default,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Value is required"
        };

        private static dynamic IsInvalid<T>(T value) => new
        {
            Condition = IsEnumInvalid(value),
            Message = "Value is not recognized"
        };

        private static dynamic IsNotSame(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate != secondDate,
                Message = $"Date is not same as {secondDateName}"
            };

        private static bool IsEnumInvalid<T>(T value)
        {
            bool isDefined = Enum.IsDefined(typeof(T), value);

            return isDefined is false;
        }

        private static void ValidateTicketEnrollmentNotNull(TicketEnrollment TicketEnrollment)
        {
            if (TicketEnrollment is null)
            {
                throw new NullTicketEnrollmentException();
            }
        }

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidTicketEnrollmentException = new InvalidTicketEnrollmentException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidTicketEnrollmentException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidTicketEnrollmentException.ThrowIfContainsErrors();
        }
    }
}