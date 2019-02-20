using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shope.Models;

namespace Shope.Models
{
    public class ShopeContext : DbContext
    {
        public ShopeContext (DbContextOptions<ShopeContext> options)
            : base(options)
        {
        }

        public DbSet<Shope.Models.Customer> Customer { get; set; }

        public DbSet<Shope.Models.Order> Order { get; set; }

        public DbSet<Shope.Models.Product> Product { get; set; }

        public DbSet<Shope.Models.OrderAndProduct> OrderAndProduct { get; set; }
    }
}
