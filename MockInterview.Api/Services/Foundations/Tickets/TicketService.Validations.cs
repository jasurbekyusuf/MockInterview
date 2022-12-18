using System;
using Microsoft.Extensions.Hosting;
using MockInterview.Api.Models.Tickets;
using MockInterview.Api.Models.Tickets.Exceptions;

namespace MockInterview.Api.Services.Foundations.Tickets
{
    public partial class TicketService
    {
        private void ValidateTicketOnAdd(Ticket ticket)
        {
            ValidateTicketIsNotNull(ticket);

            Validate(
                (Rule: IsInvalid(ticket.Id), Parameter: nameof(Ticket.Id)),
                (Rule: IsInvalid(ticket.CreatedTime), Parameter: nameof(Ticket.CreatedTime)),
                (Rule: IsInvalid(ticket.StartTime), Parameter: nameof(Ticket.StartTime)),
                (Rule: IsInvalid(ticket.EndTime), Parameter: nameof(Ticket.EndTime)),
                (Rule: IsInvalid(ticket.Speciality), Parameter: nameof(Ticket.Speciality)));
        }

        private void ValidatePostOnModify(Ticket post)
        {
            ValidateTicketIsNotNull(post);

            Validate
            (
                (Rule: IsInvalid(post.Id), Parameter: nameof(post.Id)),
                (Rule: IsInvalid(post.CreatedTime), Parameter: nameof(post.CreatedTime)),
                (Rule: IsInvalid(post.StartTime), Parameter: nameof(post.StartTime)),
                (Rule: IsInvalid(post.EndTime), Parameter: nameof(post.EndTime)),
                (Rule: IsInvalid(post.Speciality), Parameter: nameof(post.Speciality))
            );
        }

        private void ValidateTicketIsNotNull(Ticket ticket)
        {
            if (ticket is null)
            {
                throw new NullTicketException();
            }
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidTicketException =
                new InvalidTicketException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidTicketException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidTicketException.ThrowIfContainsErrors();
        }

        public void ValidateTicketId(Guid postId) =>
           Validate((Rule: IsInvalid(postId), Parameter: nameof(Ticket.Id)));

        private static void ValidateStorageTicket(Ticket maybePost, Guid postId)
        {
            if (maybePost is null)
            {
                throw new NotFoundTicketException(postId);
            }
        }
    }
}
