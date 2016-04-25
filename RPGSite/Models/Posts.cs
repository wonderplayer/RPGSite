using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPGSite.Models
{
    public class Posts
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 10)]
        public string Title { get; set; }

        [Required]
        [StringLength(3000, MinimumLength = 10)]
        public string Description { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public bool IsNews { get; set; }

        public virtual string UserID { get; set; }

        [ForeignKey("UserID")]
        public ApplicationUser User { get; set; }

        public List<Comments> Comments { get; set; }

    }
}