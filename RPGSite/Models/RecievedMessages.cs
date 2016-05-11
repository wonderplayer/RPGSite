using System.ComponentModel.DataAnnotations.Schema;

namespace RPGSite.Models
{
    public class RecievedMessages
    {
        public int ID { get; set; }

        public virtual int SentMessageID { get; set; }

        [ForeignKey("SentMessageID")]
        public SentMessages SentMessage { get; set; }

        public string UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual ApplicationUser User { get; set; }
    }
}