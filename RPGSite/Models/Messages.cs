using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RPGSite.Models
{
    public class Messages
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MaxLength(500)]
        public string Message { get; set; }

        [Timestamp]
        public DateTime DateSent { get; set; }

        public bool Read { get; set; }

        public virtual int SenderID { get; set; }

        public RegisterViewModel Sender { get; set; }

        public virtual int RecieverID { get; set; }

        public RegisterViewModel Reciever { get; set; }
    }
}