namespace PerfectSelf.WebAPI.Models
{
    using PerfectSelf.WebAPI.Common;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text.Json.Serialization;

    [Table("UnAvailability")]
    public class UnAvailability : PerfectSelfBase
    {
        public Guid ReaderUid { get; set; }
        public DateTime Date { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
    }
}
