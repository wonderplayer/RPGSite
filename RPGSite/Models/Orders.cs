using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPGSite.Models
{
    public class Orders
    {
        public int ID { get; set; }

        public DateTime OrderDate { get; set; }

        [DataType(DataType.Currency)]
        public decimal Total { get; set; }

        public string UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual ApplicationUser User { get; set; }

        public int PaymentMethodID { get; set; }

        [ForeignKey("PaymentMethodID")]
        public virtual PaymentMethods PaymentMethod { get; set; }

        public List<OrderItems> OrderItems { get; set; }
    }
}