using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Product.Core.Models;

namespace Product.Context
{
    public class ApplicationDbContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
    {
        public DbSet<Image> Images { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Options> Options { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Billboard> Billboards { get; set; }

        public DbSet<Voucher> Vouchers { get; set; }

        public DbSet<Information> Information { get; set; }
    }
}