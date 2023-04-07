namespace PerfectSelf.WebAPI.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MessageHistory")]
    public class MessageHistory : PerfectSelfBase
    {
        public Guid SenderUid { get; set; }
        public Guid ReceiverUid { get; set; }
        public Guid RoomUid { get; set; }
        public DateTime SendTime { get; set; }
        public String Message { get; set; }
    }
}
