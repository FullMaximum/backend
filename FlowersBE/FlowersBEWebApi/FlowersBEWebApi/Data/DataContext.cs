using FlowersBEWebApi.Entities;

namespace FlowersBEWebApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<UserBase> Users { get; set; }
        public DbSet<Shop> Shops { get; set; }

    }
}