using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPGSite.Models
{
    public class Orders
    {
        public int ID { get; set; }

        [Timestamp]
        public DateTime OrderDate { get; set; }

        [DataType(DataType.Currency)]
        public int Total { get; set; }

        public virtual int UserID { get; set; }

        public RegisterViewModel User { get; set; }

        public virtual int PaymentMethodID { get; set; }

        public PaymentMethods PaymentMethod { get; set; }

        public List<OrderItems> OrderItems { get; set; }
    }
}