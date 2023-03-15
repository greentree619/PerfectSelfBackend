namespace PerfectSelf.WebAPI.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Book")]
    public class Book : PerfectSelfBase
    {
        public int ActorId { get; set; }
        public int ReaderId { get; set; }
        public DateTime BookTime { get; set; }
        public String ScriptFile { get; set; }
    }
}
