﻿using Microsoft.EntityFrameworkCore;
using Nyelvtanulas.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Nyelvtanulas
{
    public class UserDbContext : DbContext
    {
        // ez egy adatbázis táblát jelöl
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1-N kapcsolatnál ide jön az 1-nél lévő
            // Kapcsoaltot fogunk definiálni a tanár táblán
            modelBuilder.Entity<User>()
                .HasOne(user => user.users)
                .WithOne(profile => profile.users)
                .HasForeignKey<User>(profile => profile.Id)
                .IsRequired(); // Kötelező kapcsolat
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Ha még nem volt konfigurálva az adatbázis
            if (!optionsBuilder.IsConfigured)
            {
                // akkor állítsuk be a connection stringet -> csatlakozás
                // (localdb)\\MSSQLLocalDB -> beépített Visual Studio lokál MSSQL server
                // StudentsDatabase -> adatbázis neve
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=StudentsDatabase");
            }
        }
    }
}
