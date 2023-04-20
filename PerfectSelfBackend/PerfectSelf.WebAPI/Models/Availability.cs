namespace PerfectSelf.WebAPI.Models
{
    using PerfectSelf.WebAPI.Common;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text.Json.Serialization;

    [Table("Availability")]
    public class Availability : PerfectSelfBase
    {
        public enum ReapeatType
        {
            EveryDay,
            EveryWeek,
            EveryMonth,
            Nothing = -1
        }
        public Guid ReaderUid { get; set; }
        public bool IsStandBy { get; set; } = false;
        public ReapeatType RepeatFlag { get; set; } = ReapeatType.Nothing;
        public DateTime Date { get; set; }

        //[JsonConverter(typeof(TimeOnlyJsonConverter))]
        public DateTime FromTime { get; set; }

        ////[JsonConverter(typeof(TimeOnlyJsonConverter))]
        public DateTime ToTime { get; set; }
    }
}
