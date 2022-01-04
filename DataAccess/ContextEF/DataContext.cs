using Microsoft.EntityFrameworkCore;
using DataAccess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DataAccess.ContextEF
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<PhoneBook> PhoneBooks { get; set; }
    }
}
