// <auto-generated />
using System;
using GoCycling.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GoCycling.Migrations
{
    [DbContext(typeof(GoCycleDbContext))]
    [Migration("20230205153006_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GoCycling.Model.Tile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("X")
                        .HasColumnType("int");

                    b.Property<int>("Y")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Tiles");
                });

            modelBuilder.Entity("GoCycling.Model.TileConquer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("TileId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TileId");

                    b.ToTable("TileConquers");
                });

            modelBuilder.Entity("GoCycling.Model.TileConquer", b =>
                {
                    b.HasOne("GoCycling.Model.Tile", null)
                        .WithMany("Conquers")
                        .HasForeignKey("TileId");
                });

            modelBuilder.Entity("GoCycling.Model.Tile", b =>
                {
                    b.Navigation("Conquers");
                });
#pragma warning restore 612, 618
        }
    }
}
