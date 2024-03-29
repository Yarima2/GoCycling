﻿using GoCycling.Models;
using Microsoft.EntityFrameworkCore;

namespace GoCycling.Queries
{
    public class GoCycleDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<TileConquer> TileConquers { get; set; }


        public static string connString;


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
			//check if code is running locally and not on azure. azure always sets this env variable
#if DEBUG
			optionsBuilder.UseLazyLoadingProxies()
                          .UseSqlite("Data Source=Database.db");
#else
            optionsBuilder.UseLazyLoadingProxies()
                          .UseSqlServer(connString);
#endif

		}
	}
}
