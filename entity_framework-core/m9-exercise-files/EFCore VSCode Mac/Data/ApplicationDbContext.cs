using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication.Data {

  public class ApplicationDbContext : IdentityDbContext<ApplicationUser>   {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)     {
    }

    protected override void OnModelCreating(ModelBuilder builder)     {
      base.OnModelCreating(builder);
      // Customize the ASP.NET Identity model and override the defaults if needed.
      // For example, you can rename the ASP.NET Identity table names and more.
      // Add your customizations after calling base.OnModelCreating(builder);
      //quick and dirty takes care of my entities not all scenarios
      foreach (var entity in builder.Model.GetEntityTypes()) {
        var currentTableName = builder.Entity(entity.Name).Metadata.Relational().TableName;
        builder.Entity(entity.Name).ToTable(currentTableName.ToLower());
      }
    }
  }
}
