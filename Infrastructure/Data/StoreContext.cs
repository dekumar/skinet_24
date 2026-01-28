using System;
using Core.Entities;
using Infrastructure.Config;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

//using primary constructor
public class StoreContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product>Products{get;set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<Product>().Property(x=>x.Price).HasColumnType("Decimal(18,2)");
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
    }
}

/*-------------------------------------------*/
/* Old way without using primary constructor */
/*-------------------------------------------*/
// public class StoreContext : DbContext
// {
//     public StoreContext(DbContextOptions options) : base(options)
//     {        
//     }
//     public DbSet<Product>Products{get;set;}
// }
/*-------------------------------------------*/
/* Old way without using primary constructor */
/*-------------------------------------------*/