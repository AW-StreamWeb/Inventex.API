using System.Net.Mime;
using Inventex.API.Management.Domain.Models;
using Inventex.API.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Inventex.API.Management.Persistence.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Machine> Machines { get; set; }
    
    public DbSet<Inventory> Inventories { get; set; }
    public AppDbContext(DbContextOptions options) : base(options){
    }
    protected override void OnModelCreating(ModelBuilder builder){
        base.OnModelCreating(builder);
        //USERS
        builder.Entity<User>().ToTable("Users");
        builder.Entity<User>().HasKey(p=>p.Id);
        builder.Entity<User>().Property(p=>p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(p=>p.Name).IsRequired().HasMaxLength(30);

        //Relationships
        builder.Entity<User>()
            .HasMany(p=>p.Machines)
            .WithOne(p=>p.User)
            .HasForeignKey(p=>p.UserId);
        
        //MACHINES
        builder.Entity<Machine>().ToTable("Machines");
        builder.Entity<Machine>().HasKey(p=>p.Id);
        builder.Entity<Machine>().Property(p=>p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Machine>().Property(p=>p.Name).IsRequired().HasMaxLength(50);
        builder.Entity<Machine>().Property(p=>p.Description).HasMaxLength(120);
        builder.Entity<Machine>().Property(p=>p.Lifetime).HasMaxLength(20);
        builder.Entity<Machine>().Property(p=>p.Active);


        //INVENTORIES

        builder.Entity<Inventory>().ToTable("Inventories");
        builder.Entity<Inventory>().HasKey(p => p.Id);
        builder.Entity<Inventory>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Inventory>().Property(p => p.Name).IsRequired().HasMaxLength(50);
        builder.Entity<Inventory>().Property(p => p.Image).HasDefaultValue();
        builder.Entity<Inventory>().Property(p => p.Price).HasMaxLength(20);
        builder.Entity<Inventory>().Property(p => p.Category).HasMaxLength(50);
        builder.Entity<Inventory>().Property(p => p.InvetoryStatus).HasMaxLength(50);
        
        //Apply Snake Case Naming Convention

        builder.UseSnakeCaseNamingConvention();
    }
    
    
}