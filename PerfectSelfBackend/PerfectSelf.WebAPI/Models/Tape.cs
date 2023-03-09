namespace PerfectSelf.WebAPI.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Tape")]
    public class Tape : PerfectSelfBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TapeId { get; set; }
        public int TapeOwner { get; set; }
        public string TapeName { get; set; }
        public string TapeKey { get; set; }
        public string BucketName { get; set; }
    }
}
