using ChinesOuctionServer.Models;

namespace ChinesOuctionServer.DAL
{
    public class HSContext: DbContext
    {   
        public HSContext(DbContextOptions<HSContext> options) : base(options)
        {
            
        }
        public DbSet<Donator> Donators { get; set; }
        public DbSet<Gift> Gifts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Winner> Winner { get; set; }
    }
}
