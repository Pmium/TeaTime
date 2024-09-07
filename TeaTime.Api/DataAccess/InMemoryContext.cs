using Microsoft.EntityFrameworkCore;
using TeaTime.Api.DataAccess.DbEntities;

namespace TeaTime.Api.DataAccess
{
    public class InMemoryContext : DbContext
    {
        public InMemoryContext(DbContextOptions<InMemoryContext> options) : base(options)
        {
        }

        public DbSet<StoreEntity> Stores { get; set; } = null!;

        public DbSet<OrderEntity> Orders { get; set; } = null!;
    }
}
