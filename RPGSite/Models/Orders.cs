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

        public virtual string UserID { get; set; }

        [ForeignKey("UserID")]
        public ApplicationUser User { get; set; }

        public virtual int PaymentMethodID { get; set; }

        [ForeignKey("PaymentMethodID")]
        public PaymentMethods PaymentMethod { get; set; }

        public List<OrderItems> OrderItems { get; set; }
    }
}