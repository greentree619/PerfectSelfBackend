namespace PerfectSelf.WebAPI.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PerfectSelf")]
    public class PerfectSelf: PerfectSelfBase
    {
        public int MajorVersion { get; set; }

        public int MinorVersion { get; set; }

        public int BuildNumber { get; set; }
    }
}
