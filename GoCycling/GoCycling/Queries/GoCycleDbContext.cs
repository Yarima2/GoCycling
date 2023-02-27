using GoCycling.Models;
using Microsoft.EntityFrameworkCore;

namespace GoCycling.Queries
{
    public class GoCycleDbContext : DbContext
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tile> Tiles { get; set; }
        public DbSet<TileConquer> TileConquers { get; set; }


        public static string connString;


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //check if code is running locally and not on azure. azure always sets this env variable
            if (String.IsNullOrEmpty(Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME")))
            {
                optionsBuilder.UseSqlite("Data Source=Database.db");
            }
            else
            {
                optionsBuilder.UseSqlServer(connString);
            }
        }
    }
}
