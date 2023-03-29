namespace PerfectSelf.WebAPI.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class BookList
    {
        public int Id { get; set; }
        public Guid RoomUid { get; set; }
        public Guid ActorUid { get; set; }
        public Guid ReaderUid { get; set; }
        public DateTime BookStartTime { get; set; }
        public DateTime BookEndTime { get; set; }
        public String ScriptFile { get; set; }
        public String ActorName { get; set; }
        public String ReaderName { get; set; }
        public Boolean ReaderIsLogin { get; set; }
        public String Title { get; set; }
        public double? HourlyPrice { get; set; }
        public ReaderProfile._Others Others { get; set; }
        public String About { get; set; }
        public String Skills { get; set; }
        public ReaderProfile._VoiceType VoiceType { get; set; }
    }
}
