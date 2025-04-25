using Microsoft.EntityFrameworkCore;
using Qatu.Domain.Entities;

namespace Qatu.Infrastructure.Persistence
{
    public class QatuDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Store> Stores { get; set; }

        public QatuDbContext(DbContextOptions<QatuDbContext> options)
            : base(options) { }
    }
}
