using Inventex.API.Management.Domain.Models;
using Inventex.API.Security.Domain.Models;
using Inventex.API.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Inventex.API.Shared.Persistence.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Machine> Machines { get; set; }
    
    public DbSet<Inventory> Inventories { get; set; }
    
    public DbSet<Finance> Finances { get; set; }
    
    public DbSet<Contact> Contacts { get; set; }
    
    
    public AppDbContext(DbContextOptions options) : base(options){
    }
    protected override void OnModelCreating(ModelBuilder builder){
        base.OnModelCreating(builder);
        //USERS
        builder.Entity<User>().ToTable("Users");
        builder.Entity<User>().HasKey(p=>p.Id);
        builder.Entity<User>().Property(p=>p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(p=>p.FirstName).IsRequired();
        builder.Entity<User>().Property(p=>p.LastName).IsRequired();
        builder.Entity<User>().Property(p=>p.Email).IsRequired().HasMaxLength(30);

        //Relationships
        builder.Entity<User>()
            .HasMany(p=>p.Machines)
            .WithOne(p=>p.User)
            .HasForeignKey(p=>p.UserId);

       
        builder.Entity<User>()
            .HasMany(p=>p.Inventories)
            .WithOne(p=>p.User)
            .HasForeignKey(p=>p.UserId);
        
        
        builder.Entity<User>()
            .HasMany(p=>p.Finances)
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
        builder.Entity<Inventory>().Property(p => p.Image).HasMaxLength(50);
        builder.Entity<Inventory>().Property(p => p.Price).HasMaxLength(20);
        builder.Entity<Inventory>().Property(p => p.Category).HasMaxLength(50);
        builder.Entity<Inventory>().Property(p => p.quantity).HasMaxLength(10);
        builder.Entity<Inventory>().Property(p => p.InvetoryStatus).HasMaxLength(50);
        
        
        //Finances

        builder.Entity<Finance>().ToTable("Finances");
        builder.Entity<Finance>().HasKey(p => p.Id);
        builder.Entity<Finance>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Finance>().Property(p => p.Name).IsRequired().HasMaxLength(50);
        builder.Entity<Finance>().Property(p => p.Day).HasMaxLength(50);
        builder.Entity<Finance>().Property(p => p.Quantity).HasMaxLength(20);
        builder.Entity<Finance>().Property(p => p.Type).HasMaxLength(50);

        //Apply Snake Case Naming Convention
        
        
        // Contacts 
        builder.Entity<Contact>().ToTable("Contacts");
        builder.Entity<Contact>().HasKey(p=>p.Id);
        builder.Entity<Contact>().Property(p=>p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Contact>().Property(p=>p.Name).IsRequired().HasMaxLength(50);
        builder.Entity<Contact>().Property(p=>p.Description).HasMaxLength(120);
        builder.Entity<Contact>().Property(p=>p.Lifetime).HasMaxLength(20);
        builder.Entity<Contact>().Property(p=>p.Active);

        builder.UseSnakeCaseNamingConvention();
    }
    
    
}