using System;
using System.ComponentModel.DataAnnotations;

namespace RPGSite.Models
{
    public class Trades
    {
        public int ID { get; set; }

        [Timestamp]
        public DateTime TradeDate { get; set; }

        [StringLength(8)]
        public string Status { get; set; }

        public virtual int SenderID { get; set; }

        public RegisterViewModel Sender { get; set; }

        public virtual int RecieverID { get; set; }

        public RegisterViewModel Reciever { get; set; }

        public virtual int WantedItemID { get; set; }

        public Equipment WantedItem { get; set; }

        public virtual int OfferedItemID { get; set; }

        public Equipment OfferedItem { get; set; }
    }
}