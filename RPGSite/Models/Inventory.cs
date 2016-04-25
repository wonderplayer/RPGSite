using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPGSite.Models
{
    public class Inventory
    {
        public int ID { get; set; }

        public int Quantity { get; set; }

        public virtual int UserID { get; set; }

        public RegisterViewModel User { get; set; }

        public virtual int EquipmentID { get; set; }

        public Equipment Equipment { get; set; }
    }
}