using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPGSite.Models
{
    public class Comments
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(255)]
        public string Comment { get; set; }

        public DateTime Created { get; set; }

        public int PostID { get; set; }

        [ForeignKey("PostID")]
        public virtual Posts Post { get; set; }

        public string UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual ApplicationUser User { get; set; }

    }
}