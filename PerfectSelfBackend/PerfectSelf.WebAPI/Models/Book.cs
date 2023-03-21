namespace PerfectSelf.WebAPI.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Book")]
    public class Book : PerfectSelfBase
    {
        public Guid ActorUid { get; set; }
        public Guid ReaderUid { get; set; }
        public DateTime BookTime { get; set; }
        public String ScriptFile { get; set; }
    }
}
