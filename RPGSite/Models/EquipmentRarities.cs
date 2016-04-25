using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPGSite.Models
{
    public class EquipmentRarities
    {

        public int ID { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string Rarity { get; set; }

        public List<Equipment> Equipment { get; set; }
    }
}