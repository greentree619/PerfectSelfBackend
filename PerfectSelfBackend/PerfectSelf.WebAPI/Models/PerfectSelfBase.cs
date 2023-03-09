namespace PerfectSelf.WebAPI.Models
{
    public class PerfectSelfBase
    {
        public bool IsDeleted { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public DateTime DeletedTime { get; set; }
    }
}
