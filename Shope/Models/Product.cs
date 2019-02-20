using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shope.Models
{
    public class Product
    {

        public int Id { get; set; }
        public String TypeName { get; set; }
        public int Price { get; set; }
        public String Color { get; set; }
        public double Weight { get; set; }
        public int Unit { get; set; }
        public int OrderId { get; set; }
        public virtual ICollection<OrderAndProduct> OrderAndProduct { get; set; }



    }
}
