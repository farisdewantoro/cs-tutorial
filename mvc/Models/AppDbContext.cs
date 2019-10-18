using Microsoft.EntityFrameworkCore;
using System;
namespace testing1.Models
{
    public class AppDbContext : DbContext
    {

        // INSTALL Microsoft.EntityFrameworkCore.SqlServer
        // INSTALL Microsoft.EntityFrameworkCore.Design
        // INSTALL Microsoft.EntityFrameworkCore.Tools
        // INSTALL Pomelo.EntityFrameworkCore.MySql 
        // INSTALL MySql.Data.EntityFrameworkCore <== untuk nyimpan di localhost 


        // UNTUK MENGGUNAKAN DOTNET MIGRATION INSTALL TOOL DOTNET EF
        // INSTALL LOCAL :
        // 1. dotnet new tool-manifest
        // 2. dotnet -d tool install  dotnet-ef

        // INSTALL GLOBAL :
        // dotnet tool install --global dotnet-ef <--- kalo sudah global gausah install2 lagi

        // KALO SUDAH INSTALL :
        // dotnet ef -h <--check help


        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        public DbSet<Employee> Employees { get; set; } //DbSet<Employee> <=== Untuk create table Employees pada database ketika migrations 

    }
}
