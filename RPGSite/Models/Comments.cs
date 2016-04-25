using System;
using System.ComponentModel.DataAnnotations;

namespace RPGSite.Models
{
    public class Comments
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(255)]
        public string Comment { get; set; }

        [Timestamp]
        public DateTime Created { get; set; }

        public virtual int PostID { get; set; }

        public Posts Post { get; set; }

        public virtual int UserID { get; set; }

        public RegisterViewModel User { get; set; }

    }
}