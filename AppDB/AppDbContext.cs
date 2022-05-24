using FidelityTest.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FidelityTest.DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}
