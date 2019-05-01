using Microsoft.EntityFrameworkCore;
using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
        }

        public DbSet<Food> Foods { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

    }
}
