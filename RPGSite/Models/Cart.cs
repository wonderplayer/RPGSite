using System;
using System.ComponentModel.DataAnnotations;

namespace RPGSite.Models
{
    public class Cart
    {
        [Key]
        public int RecordID { get; set; }

        public string CartID { get; set; }

        public int EquipmentID { get; set; }

        public int Count { get; set; }

        public DateTime DateCreated { get; set; }

        public virtual Equipment Equipment { get; set; }
    }
}