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
        public DbSet<Player> Players { get; set; } = null!;
        public DbSet<Upgrade> Upgrades { get; set; } = null!;
        public DbSet<Link> Links { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {//ID's
            modelBuilder.Entity<Player>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<Link>()
                .HasKey(l => l.Id);
            modelBuilder.Entity<Upgrade>()
                .HasKey(u => u.Id);
            //Constrains
            modelBuilder.Entity<Player>()
                .HasIndex(p => p.GameSlot)
                .IsUnique();
            //Delete Behavior
            modelBuilder.Entity<Link>()
                .HasOne(l => l.player)
                .WithMany(p => p.Links)
                .HasForeignKey(l => l.playerId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Link>()
                .HasOne(l => l.upgrade)
                .WithMany()
                .HasForeignKey(l => l.upgradeId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Link>()
                .HasOne(l => l.player)
                .WithMany(p => p.Links)
                .HasForeignKey(p => p.playerId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($@"Data Source={Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\source\repos\MasterMinerV1\MasterMinerV1\Database\Database.db");
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}
