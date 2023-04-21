namespace PerfectSelf.WebAPI.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    //[Table("MessageChannelView")]
    public class MessageChannelView
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid SenderUid { get; set; }
        public String SenderName { get; set; }
        public Guid ReceiverUid { get; set; }
        public String ReceiverName { get; set; }
        public Guid RoomUid { get; set; }
        public DateTime SendTime { get; set; }
        public bool HadRead { get; set; }
        public String Message { get; set; }
        public String SenderAvatarBucket { get; set; }
        public String SenderAvatarKey { get; set; }
        public String ReceiverAvatarBucket { get; set; }
        public String ReceiverAvatarKey { get; set; }
        public bool SenderIsOnline { get; set; }
        public bool ReceiverIsOnline { get; set; } 
    }
}
