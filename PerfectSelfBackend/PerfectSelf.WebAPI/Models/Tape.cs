namespace PerfectSelf.WebAPI.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Tape")]
    public class Tape : PerfectSelfBase
    {
        public Guid ReaderUid { get; set; }//May use uid for actor.
        public string TapeName { get; set; }
        public string BucketName { get; set; }
        public string TapeKey { get; set; }
        public string RoomUid { get; set; }
        public string TapeId { get; set; } = DateTime.Now.ToString("HHmmssf");
    }
}
