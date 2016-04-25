using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RPGSite.Models
{
    public class Gallery
    {
        public int ID { get; set; }

        [MaxLength(4096)]
        public byte[] Picture { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public virtual int UserID { get; set; }

        public RegisterViewModel User { get; set; }

    }
}