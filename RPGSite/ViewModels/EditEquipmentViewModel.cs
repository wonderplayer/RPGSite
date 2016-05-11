using RPGSite.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPGSite.ViewModels
{
    public class EditEquipmentViewModel
    {
        public int ID { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public string Picture { get; set; }

        [Required]
        public int TypeID { get; set; }

        [ForeignKey("TypeID")]
        public virtual EquipmentTypes Type { get; set; }

        [Required]
        public int RarityID { get; set; }

        [ForeignKey("RarityID")]
        public virtual EquipmentRarities Rarity { get; set; }
    }
}