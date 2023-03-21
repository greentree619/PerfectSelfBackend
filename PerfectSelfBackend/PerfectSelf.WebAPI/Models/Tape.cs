namespace PerfectSelf.WebAPI.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Tape")]
    public class Tape : PerfectSelfBase
    {
        public Guid ReaderUid { get; set; }
        public string TapeName { get; set; }
        public string BucketName { get; set; }
        public string TapeKey { get; set; }
    }
}
