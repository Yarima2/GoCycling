﻿// <auto-generated />
using System;
using GoCycling.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GoCycling.Migrations
{
    [DbContext(typeof(GoCycleDbContext))]
    [Migration("20230227120211_addedUser+Team+Token")]
    partial class addedUserTeamToken
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.2");

            modelBuilder.Entity("GoCycling.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("GoCycling.Models.Tile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("X")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Y")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Tiles");
                });

            modelBuilder.Entity("GoCycling.Models.TileConquer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Encircled")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TileId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TileId");

                    b.HasIndex("UserId");

                    b.ToTable("TileConquers");
                });

            modelBuilder.Entity("GoCycling.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("TeamId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GoCycling.Models.TileConquer", b =>
                {
                    b.HasOne("GoCycling.Models.Tile", "Tile")
                        .WithMany("Conquers")
                        .HasForeignKey("TileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GoCycling.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tile");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GoCycling.Models.User", b =>
                {
                    b.HasOne("GoCycling.Models.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Team");
                });

            modelBuilder.Entity("GoCycling.Models.Tile", b =>
                {
                    b.Navigation("Conquers");
                });
#pragma warning restore 612, 618
        }
    }
}
