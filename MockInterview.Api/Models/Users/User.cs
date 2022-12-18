//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to use to bring order in your workplace
//==================================================

using Microsoft.AspNetCore.Identity;
using System;

namespace MockInterview.Api.Models.Users
{
    public class User: IdentityUser<Guid>
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Role { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
