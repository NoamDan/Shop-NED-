using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shope.Models
{
    public class Product
    {

        public int Id { get; set; }
        [Display(Name = "סוג תכשיט")]
        public String TypeName { get; set; }
        [Display(Name = "מחיר")]
        public int Price { get; set; }
        [Display(Name = "צבע")]
        public String Color { get; set; }
        [Display(Name = "משקל")]
        public double Weight { get; set; }
        [Display(Name = "כמות")]
        public int Unit { get; set; }
        public int OrderId { get; set; }
        public virtual ICollection<OrderAndProduct> OrderAndProduct { get; set; }



    }
}
