using Microsoft.EntityFrameworkCore;
using PhoneBookApp.WebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookApp.WebSite.ContextEF
{
    public class DataContext : DbContext
    {
        public DbSet<PhoneBook> PhoneBooks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
            @"Server=(local);  
              DataBase=DataPhoneBook;
              Trusted_Connection=True;"
            );
        }
    }
}
