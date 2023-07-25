namespace PerfectSelf.WebAPI.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Tape")]
    public class Tape : PerfectSelfBase
    {
        public Guid ReaderUid { get; set; }//May use uid for actor.
        public string TapeName { get; set; }//[Folder], Otherwise Folder Name
        public string BucketName { get; set; }//[Folder], 0 - Folder from overlay, 1 - Folder from user 
        public string TapeKey { get; set; }
        public string RoomUid { get; set; }//[Folder] Tape ID to be overlapped.
        public string TapeId { get; set; } = DateTime.Now.ToString("HHmmssf");
        public string ParentId { get; set; } = "";//Parent Folder ID.
    }
}
