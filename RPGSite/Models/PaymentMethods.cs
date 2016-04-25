using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPGSite.Models
{
    public class PaymentMethods
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Method { get; set; }

        public List<Orders> Orders { get; set; }
    }
}