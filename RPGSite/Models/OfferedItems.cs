using System.ComponentModel.DataAnnotations.Schema;

namespace RPGSite.Models
{
    public class OfferedItems
    {
        public int ID { get; set; }

        public virtual int TradeID { get; set; }

        [ForeignKey("TradeID")]
        public Trades Trade { get; set; }

        public virtual int ItemID { get; set; }

        [ForeignKey("ItemID")]
        public Equipment Equipment { get; set; }

        public virtual string UserID { get; set; }

        [ForeignKey("UserID")]
        public ApplicationUser User { get; set; }
    }
}