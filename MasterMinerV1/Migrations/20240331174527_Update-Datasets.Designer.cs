﻿// <auto-generated />
using MasterMinerV1.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MasterMinerV1.Migrations
{
    [DbContext(typeof(DBconfig))]
    [Migration("20240331174527_Update-Datasets")]
    partial class UpdateDatasets
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.27");

            modelBuilder.Entity("MasterMinerV1.Database.Link", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("playerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("upgradeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("playerId");

                    b.HasIndex("upgradeId");

                    b.ToTable("Links");
                });

            modelBuilder.Entity("MasterMinerV1.Database.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClickVal")
                        .HasColumnType("INTEGER");

                    b.Property<int>("GameSlot")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Ores")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TotalClicks")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("GameSlot")
                        .IsUnique();

                    b.ToTable("Players");
                });

            modelBuilder.Entity("MasterMinerV1.Database.Upgrade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClickVal")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Cost")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IncreasePercent")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Upgrades");
                });

            modelBuilder.Entity("MasterMinerV1.Database.Link", b =>
                {
                    b.HasOne("MasterMinerV1.Database.Player", "player")
                        .WithMany("Links")
                        .HasForeignKey("playerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MasterMinerV1.Database.Upgrade", "upgrade")
                        .WithMany()
                        .HasForeignKey("upgradeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("player");

                    b.Navigation("upgrade");
                });

            modelBuilder.Entity("MasterMinerV1.Database.Player", b =>
                {
                    b.Navigation("Links");
                });
#pragma warning restore 612, 618
        }
    }
}
