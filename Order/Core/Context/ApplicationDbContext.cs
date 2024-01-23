using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Order.core.Models;

namespace Order.core.Context
{
    public class ApplicationDbContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Models.Order> Orders { get; set; }
    }
}