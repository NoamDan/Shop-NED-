using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shope.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Display(Name = "שם פרטי")]
        public String Fname { get; set; }
        [Display(Name = "שם משפחה")]
        public String Lnam { get; set; }
        [Display(Name = "עיר")]
        public String City { get; set; }
        [Display(Name = "רחוב")]
        public String Street { get; set; }
        [Display(Name = "מספר בית")]   
        public int NumberHome{ get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public  int IsAdmin { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }

}

