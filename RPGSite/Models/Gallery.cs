using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPGSite.Models
{
    public class Gallery
    {
        public int ID { get; set; }

        public string Picture { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        public string UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual ApplicationUser User { get; set; }

    }
}