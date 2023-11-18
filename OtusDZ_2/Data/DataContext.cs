using Microsoft.EntityFrameworkCore;
using OtusDZ_2.Models;
using System.Collections.Generic;
using WebApi.Entities;

namespace OtusDZ_2.Data
{
    public class DataContext : DbContext
    {
        public DbSet<CustomerEntity> Customers { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                new Customer() { Id = 1, Firstname = "Сергей", Lastname = "Орлов" },
                new Customer() { Id = 2, Firstname = "Андрей", Lastname = "Борисов" }
                );
        }

    }
}
