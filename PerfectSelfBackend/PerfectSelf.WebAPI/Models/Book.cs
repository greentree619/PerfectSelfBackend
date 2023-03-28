namespace PerfectSelf.WebAPI.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Book")]
    public class Book : PerfectSelfBase
    {
        public Guid ActorUid { get; set; }
        public Guid ReaderUid { get; set; }
        public Guid RoomUid { get; set; } = Guid.NewGuid();
        public DateTime BookStartTime { get; set; }
        public DateTime BookEndTime { get; set; }
        public String ScriptFile { get; set; }
    }
}
