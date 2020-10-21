using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using ShopPlatform.Models.Accounting;
using ShopPlatform.Models.Items;
using ShopPlatform.Models.Shop;

namespace ShopPlatform.Models
{
    public class DatabaseContext:DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Password> Passwords { get; set; }
        public DbSet<TokensChain> TokenChains { get; set; }
        public DbSet<Shop.Shop> Shops { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<ItemIcon> ItemIcons { get; set; }
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
            string citiesFile = "cities.json";
            List<Location> locations = null;
            if (File.Exists(citiesFile))
            {
                using (StreamReader sr = new StreamReader(citiesFile))
                {
                    var json = sr.ReadToEnd().Replace(Environment.NewLine, "");
                    locations = JArray.Parse(json).Select(x => { return new Location(x);}).ToList();
                }
            }
            modelBuilder.Entity<Location>().HasData(locations);
            base.OnModelCreating(modelBuilder);
        }
    }
}
