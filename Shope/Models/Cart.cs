using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shope.Models
{
    public class Cart
    {
        virtual public List<Product> Products { get; set; }
        virtual public int? TotalAmount { get; set; }
        virtual public List<int> Count { get; set; }
        public Cart()
        {
            this.Products = new List<Product>();
            this.TotalAmount = 0;
            this.Count = new List<int>();
        }
    }
}


