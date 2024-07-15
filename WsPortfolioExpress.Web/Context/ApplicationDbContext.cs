using Microsoft.EntityFrameworkCore;
using WsPortfolioExpress.Common.Entities;

namespace WsPortfolioExpress.Web.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
