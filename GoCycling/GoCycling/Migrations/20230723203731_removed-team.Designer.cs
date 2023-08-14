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
    [Migration("20230723203731_removed-team")]
    partial class removedteam
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true);

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

                    b.Property<long>("ActivityId")
                        .HasColumnType("INTEGER");

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
                        .HasColumnType("INTEGER");

                    b.Property<int>("TokenId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TokenId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GoCycling.Models.UserToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("access_token")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("expires_at")
                        .HasColumnType("INTEGER");

                    b.Property<string>("refresh_token")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("token_type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("UserTokens");
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
                    b.HasOne("GoCycling.Models.UserToken", "Token")
                        .WithMany()
                        .HasForeignKey("TokenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Token");
                });

            modelBuilder.Entity("GoCycling.Models.Tile", b =>
                {
                    b.Navigation("Conquers");
                });
#pragma warning restore 612, 618
        }
    }
}
