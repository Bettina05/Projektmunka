﻿using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Nyelvtanulas.Models
{
    public class UserDbContext : DbContext
    {
        // ez egy adatbázis táblát jelöl
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1-N kapcsolatnál ide jön az 1-nél lévő
            /*modelBuilder.Entity<User>()
                .HasOne(usering => usering.users)
                .WithOne(profile => profile.users)
                .HasForeignKey<User>(profile => profile.Id)
                .IsRequired(); 
                // Kötelező kapcsolat
            */
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Ha még nem volt konfigurálva az adatbázis
            if (!optionsBuilder.IsConfigured)
            {
                // UsersDatabase -> adatbázis neve
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=UsersDatabase");
            }
        }
    }
}
