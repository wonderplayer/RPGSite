using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPGSite.Models
{
    public class Trades
    {
        public int ID { get; set; }

        public DateTime TradeDate { get; set; }

        [StringLength(8)]
        public string Status { get; set; }

        public List<OfferedItems> OfferedItems { get; set; }

        public List<WantedItems> RecieverItems { get; set; }
    }
}