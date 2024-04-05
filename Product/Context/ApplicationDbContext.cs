using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Product.Core.Models;

namespace Product.Context
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Image> Images { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Options> Options { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Billboard> Billboards { get; set; }

        public DbSet<Voucher> Vouchers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Reviews> Reviews { get; set; }

        public DbSet<Blog> Blogs { get; set; }

    }
}