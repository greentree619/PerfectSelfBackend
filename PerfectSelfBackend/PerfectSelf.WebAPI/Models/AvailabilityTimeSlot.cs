namespace PerfectSelf.WebAPI.Models
{
    using static PerfectSelf.WebAPI.Models.Availability;

    public class AvailabilityTimeSlot
    {
        public bool IsStandBy { get; set; } = false;
        public ReapeatType RepeatFlag { get; set; } = ReapeatType.Nothing;
        public DateTime Date { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
    }

    public class AvailabilityTimeSlotBatch
    {
        public Guid ReaderUid { get; set; }
        public List<AvailabilityTimeSlot> batchTimeSlot { get; set; }
    }
}
