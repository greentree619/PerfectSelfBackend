namespace PerfectSelf.WebAPI.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ReaderSessionCount
    {
        public Guid Uid { get; set; }
        public int SessionCount { get; set; }
    }
}
