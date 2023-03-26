namespace PerfectSelf.WebAPI.Models
{
    using PerfectSelf.WebAPI.Common;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text.Json.Serialization;

    [Table("Availability")]
    public class Availability : PerfectSelfBase
    {
        public Guid ReaderUid { get; set; }

        public DateTime Date { get; set; }

        //[JsonConverter(typeof(TimeOnlyJsonConverter))]
        public DateTime FromTime { get; set; }

        ////[JsonConverter(typeof(TimeOnlyJsonConverter))]
        public DateTime ToTime { get; set; }
    }
}
