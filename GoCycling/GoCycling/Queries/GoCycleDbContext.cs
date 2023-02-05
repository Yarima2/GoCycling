using GoCycling.Model;
using Microsoft.EntityFrameworkCore;

namespace GoCycling.Queries
{
    public class GoCycleDbContext : DbContext
    {
        public DbSet<Tile> Tiles { get; set; }
        public DbSet<TileConquer> TileConquers { get; set; }


        public static string connString;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //check if code is running locally and not on azure. azure always sets this env variable
            /*if (String.IsNullOrEmpty(Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME")))
            {
                optionsBuilder.UseSqlite();
            }
            else
            {*/
                optionsBuilder.UseSqlServer(connString);
            //}
        }
    }
}
