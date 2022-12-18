//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to use to bring order in your workplace
//==================================================

using System;

namespace MockInterview.Api.Models.Tickets
{
    public class Ticket
    {
        public Guid Id { get; set; }

        public string InterviewerId { get; set; }

        public DateTime CreatedTime { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public TicketStatus Status { get; set; }

        public string Speciality { get; set; }
    }
}
