using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPGSite.Models
{
    public class EquipmentTypes
    {

        public int ID { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Type { get; set; }

        public List<Equipment> Equipment { get; set; }
    }
}