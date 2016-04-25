using System.ComponentModel.DataAnnotations.Schema;

namespace RPGSite.Models
{
    public class Inventories
    {
        public int ID { get; set; }

        public int Quantity { get; set; }

        public virtual string UserID { get; set; }

        [ForeignKey("UserID")]
        public ApplicationUser User { get; set; }

        public virtual int EquipmentID { get; set; }

        [ForeignKey("EquipmentID")]
        public Equipment Equipment { get; set; }
    }
}