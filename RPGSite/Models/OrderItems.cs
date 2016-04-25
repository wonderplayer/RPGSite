using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RPGSite.Models
{
    public class OrderItems
    {
        public int ID { get; set; }

        public int Quantity { get; set; }

        [DataType(DataType.Currency)]
        public int Total { get; set; }

        public virtual int EquipmentID { get; set; }

        public Equipment Equipment { get; set; }

        public virtual int OrderID { get; set; }

        public Orders Order { get; set; }
    }
}