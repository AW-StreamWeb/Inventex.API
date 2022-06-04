using Inventex.API.Management.Domain.Models;
using Inventex.API.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Inventex.API.Management.Persistence.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Machine> Machines { get; set; }
    public AppDbContext(DbContextOptions options) : base(options){

    }
    protected override void OnModelCreating(ModelBuilder builder){
        base.OnModelCreating(builder);
        builder.Entity<Category>().ToTable("Categories");
        builder.Entity<Category>().HasKey(p=>p.Id);
        builder.Entity<Category>().Property(p=>p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Category>().Property(p=>p.Name).IsRequired().HasMaxLength(30);

        //Relationsships
        builder.Entity<Category>()
            .HasMany(p=>p.Machines)
            .WithOne(p=>p.Category)
            .HasForeignKey(p=>p.CategoryId);

        builder.Entity<Machine>().ToTable("Machines");
        builder.Entity<Machine>().HasKey(p=>p.Id);
        builder.Entity<Machine>().Property(p=>p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Machine>().Property(p=>p.Name).IsRequired().HasMaxLength(50);
        builder.Entity<Machine>().Property(p=>p.Description).HasMaxLength(120);
        builder.Entity<Machine>().Property(p=>p.Lifetime).HasMaxLength(20);
        builder.Entity<Machine>().Property(p=>p.Active);


        //Apply Snake Case Naming Convention

        builder.UseSnakeCaseNamingConvention();
    }
}