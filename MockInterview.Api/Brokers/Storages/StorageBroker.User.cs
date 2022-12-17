using Microsoft.EntityFrameworkCore;
using MockInterview.Api.Models.Users;
using System.Net.Sockets;

namespace MockInterview.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        DbSet<User> Users { get; set; }
    }
}
