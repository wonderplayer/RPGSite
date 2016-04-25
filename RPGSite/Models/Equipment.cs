using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPGSite.Models
{
    public class Equipment
    {
        public int ID { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        public int Price { get; set; }

        [Required]
        public byte[] Picture { get; set; }

        [Required]
        public virtual int TypeID { get; set; }

        public EquipmentTypes Type { get; set; }

        [Required]
        public virtual int RarityID { get; set; }

        public EquipmentRarities Rarity { get; set; }

        public List<OrderItems> OrderItems { get; set; }

        public List<Trades> WantedForTrade { get; set; }

        public List<Trades> OfferedForTrade { get; set; }

        public List<Inventory> Inventory { get; set; }
    }
}