using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shope.Models
{
    public class Order
    {

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderAndProduct> OrderAndProduct { get; set; }
        public  Order()
        {
            this.CustomerId = Global.sessionID;
        }
        public bool UpdateOrderId()
        {
            if (this.Id>0) {
                Global.CurrenOrderId = this.Id;
                Global.flag = true;
                return true;
            }
            return false;
           
        }
    }
}
