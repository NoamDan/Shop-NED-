using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shope.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public String Fname { get; set; }
        public String Lnam { get; set; }
        public String City { get; set; }
        public String Street { get; set; }
        public int NumberHome { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}
