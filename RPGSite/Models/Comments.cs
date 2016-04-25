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

        public virtual int PostID { get; set; }

        [ForeignKey("PostID")]
        public Posts Post { get; set; }

        public virtual string UserID { get; set; }

        [ForeignKey("UserID")]
        public ApplicationUser User { get; set; }

    }
}