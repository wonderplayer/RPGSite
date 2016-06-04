using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPGSite.Models
{
    public class Offers
    {
        [Key]
        public int ID { get; set; }

        public int WantedItemID { get; set; }

        [ForeignKey("WantedItemID")]
        public virtual WantedItem WantedItem { get; set; }

        public int OfferedItemID { get; set; }

        [ForeignKey("OfferedItemID")]
        public virtual OfferedItem OfferedItem { get; set; }

        public string OfferStatus { get; set; }
    }
}