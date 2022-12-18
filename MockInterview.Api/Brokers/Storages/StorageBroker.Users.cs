//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to use to bring order in your workplace
//==================================================

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MockInterview.Api.Models.Users;

namespace MockInterview.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        DbSet<User> Users { get; set; }

        public async ValueTask<User> InsertUserAsync(User user) =>
            await InsertAsync(user);

        public async ValueTask<User> UpdateUserAsync(User user) =>
            await UpdateAsync(user);
    }
}
