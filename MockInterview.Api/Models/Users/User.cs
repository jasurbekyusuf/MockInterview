using System;

namespace MockInterview.Api.Models.Users
{
    public class User
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Role { get; set; }
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public DateTime CreateDate { get; set; }

        public string PasswordHash { get; set; }
    }
}
