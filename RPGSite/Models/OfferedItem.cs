using System.ComponentModel.DataAnnotations.Schema;

namespace RPGSite.Models
{
    public class OfferedItem
    {
        [ForeignKey("Offer")]
        public int ID { get; set; }

        public int EquipmentID { get; set; }

        [ForeignKey("EquipmentID")]
        public virtual Equipment Equipment { get; set; }

        public string UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual ApplicationUser User { get; set; }

        public virtual Offers Offer { get; set; }
    }
}