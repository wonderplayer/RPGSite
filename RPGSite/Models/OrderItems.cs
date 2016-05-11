using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RPGSite.Models
{
    public class OrderItems
    {
        public int ID { get; set; }

        public int Quantity { get; set; }

        [DataType(DataType.Currency)]
        public decimal Total { get; set; }

        public int EquipmentID { get; set; }

        [ForeignKey("EquipmentID")]
        public virtual Equipment Equipment { get; set; }

        public int OrderID { get; set; }

        [ForeignKey("OrderID")]
        public virtual Orders Order { get; set; }
    }
}