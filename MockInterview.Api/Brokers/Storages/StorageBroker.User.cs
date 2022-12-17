//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to use to bring order in your workplace
//==================================================

using Microsoft.EntityFrameworkCore;
using MockInterview.Api.Models.Users;

namespace MockInterview.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        DbSet<User> Users { get; set; }
    }
}
