using FlowersBEWebApi.Entities;

namespace FlowersBEWebApi.Data
{
    public class DataContext : DbContext
    {
        public IConfiguration Configuration { get; set; }
        public DataContext(IConfiguration configuration) 
        {
            Configuration = configuration;
        }
        public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : base(options) 
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                var connString = Configuration.GetConnectionString("Fullmaximum");
                options.UseMySql(connString, ServerVersion.AutoDetect(connString));
            }
        }

        public DbSet<UserBase>? Users { get; set; }
        public DbSet<User>? AppUsers { get; set; }
        public DbSet<Flower>? Flowers { get; set; }
        public DbSet<Shop>? Shops { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<OrderItem>? OrderItems { get; set; }
    }
}