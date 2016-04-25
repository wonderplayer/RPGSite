using System;
using System.ComponentModel.DataAnnotations;

namespace RPGSite.Models
{
    public class Events
    {

        public int ID { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 10)]
        public string Title { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 10)]
        public string Description { get; set; }

        [Timestamp]
        public DateTime Created { get; set; }

        [Timestamp]
        public DateTime Updated { get; set; }

        [Required]
        [Timestamp]
        public DateTime StartDate { get; set; }

        [Required]
        [Timestamp]
        public DateTime EndDate { get; set; }

        public virtual int UserID { get; set; }

        public RegisterViewModel User { get; set; }

    }
}