using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopPlatform.Models.Accounting;

namespace ShopPlatform.Models
{
    public class DatabaseContext:DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Password> Passwords { get; set; }
        public DbSet<TokensChain> TokenChains { get; set; }
        public DatabaseContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
