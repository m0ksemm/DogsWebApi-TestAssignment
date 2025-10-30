using DogsWebApi.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsWebApi.Infrastructure.ApplicationDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

        public DbSet<Dog> Dogs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Dog>(b =>
            {
                b.HasKey(d => d.DogID);
                b.HasIndex(d => d.Name).IsUnique();
                b.Property(d => d.Name).IsRequired();
                b.Property(d => d.Color).IsRequired();
            });

            modelBuilder.Entity<Dog>().HasData(
                new Dog { DogID = Guid.NewGuid(), Name = "Neo", Color = "red&amber", TailLength = 22, Weight = 32 },
                new Dog { DogID = Guid.NewGuid(), Name = "Jessy", Color = "black&white", TailLength = 7, Weight = 14 }
            );
        }
    }
}
