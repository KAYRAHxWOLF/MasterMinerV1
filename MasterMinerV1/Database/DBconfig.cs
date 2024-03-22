using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMinerV1.Database
{
    internal class DBconfig : DbContext
    {
        public DbSet<> entity { get; set; } = null!;
        public DbSet<> entities { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<entity>().HasKey(A => A.id);
            modelBuilder.Entity<entities>().HasKey(B => B.id);
            base.OnModelCreating(modelBuilder);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($@"Data Source={Directory.GetCurrentDirectory()}\Database\DB.db");
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}
