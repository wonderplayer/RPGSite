using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPGSite.Models
{
    public class SentMessages
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MaxLength(500)]
        public string Message { get; set; }

        public DateTime DateSent { get; set; }

        public bool Read { get; set; }

        public virtual string UserID { get; set; }

        [ForeignKey("UserID")]
        public ApplicationUser User { get; set; }

        public List<RecievedMessages> Recievers { get; set; }
    }
}