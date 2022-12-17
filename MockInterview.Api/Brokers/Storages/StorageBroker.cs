using EFxceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace MockInterview.Api.Brokers.Storages
{
    public partial class StorageBroker : EFxceptionsContext
    {
        private readonly IConfiguration configuration;

        public StorageBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.Database.Migrate();
        }

        private async ValueTask<T> InsertAsync<T>(T @object)
        {
            var broker = new StorageBroker(this.configuration);
            broker.Entry(@object).State = EntityState.Added;
            await broker.SaveChangesAsync();

            return @object;
        }

        private IQueryable<T> SelectAll<T>() where T : class
        {
            using var broker = new StorageBroker(this.configuration);

            return broker.Set<T>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = this.configuration.GetConnectionString(name: "DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }   

        public override void Dispose() { }
    }
}