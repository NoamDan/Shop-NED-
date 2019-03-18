using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shope.Models
{
    public class OrderAndProduct
    {
        public int OrderAndProductId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public Product Product { get; set; }
        public OrderAndProduct()
        {
            if (Global.CurrentCart.TotalAmount > 0)
            {
                Global.flag = Global.ord.UpdateOrderId();
                if (Global.flag)
                {
                    this.OrderId = Global.CurrenOrderId;
                    this.ProductId = Global.CurrentCart.Products.FirstOrDefault().Id;
                    this.Count = Global.CurrentCart.Products.FirstOrDefault().Unit;
                }
            }
        }
    }
}
