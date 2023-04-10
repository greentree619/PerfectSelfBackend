namespace PerfectSelf.WebAPI.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MessageChannelView")]
    public class MessageChannelView
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid SenderUid { get; }
        public String SenderName { get; }
        public Guid ReceiverUid { get; }
        public String ReceiverName { get; }
        public Guid RoomUid { get; }
        public DateTime SendTime { get; }
        public bool HadRead { get; }
        public String Message { get; }
        public String SenderAvatarBucket { get; }
        public String SenderAvatarKey { get; }
        public String ReceiverAvatarBucket { get; }
        public String ReceiverAvatarKey { get; }
        public bool SenderIsOnline { get; }
        public bool ReceiverIsOnline { get; }
    }
}
