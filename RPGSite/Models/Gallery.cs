using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPGSite.Models
{
    public class Gallery
    {
        public int ID { get; set; }

        public byte[] Picture { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public virtual string UserID { get; set; }

        [ForeignKey("UserID")]
        public ApplicationUser User { get; set; }

    }
}