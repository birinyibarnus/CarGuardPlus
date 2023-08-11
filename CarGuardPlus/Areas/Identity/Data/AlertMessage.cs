using System.ComponentModel.DataAnnotations.Schema;

namespace CarGuardPlus.Areas.Identity.Data
{
    public class AlertMessage
    {
        public int AlertMessageId { get; set; }
        public string Message { get; set; }

        [ForeignKey("SenderUser")]
        public string SenderUserId { get; set; }
        public ApplicationUser SenderUser { get; set; }

        [ForeignKey("ReceiverUser")]
        public string ReceiverUserId { get; set; }
        [NotMapped]
        public ApplicationUser ReceiverUser { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
