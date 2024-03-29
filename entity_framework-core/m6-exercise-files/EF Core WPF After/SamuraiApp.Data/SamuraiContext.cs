﻿using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SamuraiApp.Domain;

namespace SamuraiApp.Data
{
  public class SamuraiContext : DbContext
  {
    public DbSet<Samurai> Samurais { get; set; }
    public DbSet<Battle> Battles { get; set; }
    public DbSet<Quote> Quotes { get; set; }
    public DbSet<SamuraiBattle> SamuraiBattles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {

      modelBuilder.Entity<SamuraiBattle>()
        .HasKey(s => new { s.BattleId, s.SamuraiId });

      //modelBuilder.Entity<Samurai>().Property<DateTime>("LastModified");

      foreach (var entityType in modelBuilder.Model.GetEntityTypes()) {
        modelBuilder.Entity(entityType.Name).Property<DateTime>("LastModified");
        modelBuilder.Entity(entityType.Name).Ignore("IsDirty"); 
      }
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
      optionsBuilder.UseSqlServer(
        "Server = (localdb)\\mssqllocaldb; Database = SamuraiWpfData; Trusted_Connection = True; ",
        options => options.MaxBatchSize(30));
      optionsBuilder.EnableSensitiveDataLogging();
    }

    public override int SaveChanges() {
      foreach (var entry in ChangeTracker.Entries()
       .Where(e => e.State == EntityState.Added ||
                   e.State == EntityState.Modified)) {
        entry.Property("LastModified").CurrentValue = DateTime.Now;
     }
      return base.SaveChanges();
    }
  }
}