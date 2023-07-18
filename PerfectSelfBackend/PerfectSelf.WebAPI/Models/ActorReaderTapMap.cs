namespace PerfectSelf.WebAPI.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ActorReaderTapMap
    {
        public int ActorId { get; set; }
        public String BucketName { get; set; }
        public String TapeName { get; set; }
        public String ActorTapeKey { get; set; }
        public String? ReaderTapeKey { get; set; }
        public String RoomUid { get; set; }
        public String TapeId { get; set; }
        public String? ReaderName { get; set; }
        public Guid ActorUid { get; set; }
        public Guid? ReaderUid { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public DateTime DeletedTime { get; set; }
    }
}
