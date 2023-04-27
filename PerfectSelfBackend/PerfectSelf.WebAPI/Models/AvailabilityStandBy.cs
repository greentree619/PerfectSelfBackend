namespace PerfectSelf.WebAPI.Models
{
    using PerfectSelf.WebAPI.Common;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text.Json.Serialization;

    [Table("AvailabilityStandBy")]
    public class AvailabilityStandBy : PerfectSelfBase
    {
        public enum ReapeatType
        {
            EveryDay,
            EveryWeek,
            EveryMonth,
            Nothing = -1
        }
        public Guid ReaderUid { get; set; }
        public ReapeatType RepeatFlag { get; set; } = ReapeatType.Nothing;
        public DateTime Date { get; set; }
    }
}
