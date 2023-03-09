namespace PerfectSelf.WebAPI.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PerfectSelf")]
    public class PerfectSelf: PerfectSelfBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PerfectSelfId { get; set; }

        public int MajorVersion { get; set; }

        public int MinorVersion { get; set; }

        public int BuildNumber { get; set; }
    }
}
