using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPGSite.Models
{
    public class Offers
    {
        [Key]
        public string ID { get; set; }

        public string WantedItemID { get; set; }

        [ForeignKey("WantedItemID")]
        public virtual WantedItem WantedItem { get; set; }

        public string OfferedItemID { get; set; }

        [ForeignKey("OfferedItemID")]
        public virtual OfferedItem OfferedItem { get; set; }

        public string OfferStatus { get; set; }
    }
}