using Microsoft.EntityFrameworkCore;
using SavingsAccountWebAPI.Model;

namespace SavingsAccountWebAPI.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
