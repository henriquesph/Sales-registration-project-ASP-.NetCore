using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Models;

// Acesso ao banco - poderia ser um repositories 
namespace SalesWebMVC.Data
{
    public class SalesWebMVCContext : DbContext
    {
        public SalesWebMVCContext (DbContextOptions<SalesWebMVCContext> options)
            : base(options)
        {
        }

        // Modelos sendo reconhecidos pelo Entity Framework, adicionando as classes DBSet na classe DBContext
        public DbSet<Department> Department { get; set; } = default!;
        public DbSet<Seller> Seller { get; set; } = default!;
        public DbSet<SalesRecord> SalesRecord { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder) // Corrige o erro na hora de criar o BD que não gerou as FKs
        {
            base.OnModelCreating(modelBuilder);
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
