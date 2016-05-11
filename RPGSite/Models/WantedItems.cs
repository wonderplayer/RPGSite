using System.ComponentModel.DataAnnotations.Schema;

namespace RPGSite.Models
{
    public class WantedItems
    {
        public int ID { get; set; }

        public int TradeID { get; set; }

        [ForeignKey("TradeID")]
        public virtual Trades Trade { get; set; }

        public int ItemID { get; set; }

        [ForeignKey("ItemID")]
        public virtual Equipment Equipment { get; set; }

        public string UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual ApplicationUser User { get; set; }
    }
}