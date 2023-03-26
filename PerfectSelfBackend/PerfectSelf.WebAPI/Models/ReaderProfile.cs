namespace PerfectSelf.WebAPI.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ReaderProfile")]
    public class ReaderProfile : PerfectSelfBase
    {
        public enum _VoiceType
        {
            DeepVoice,
            RaspyVoice,
            HighVoice,
            Nothing
        }

        public enum _Others
        {
            Archetype,
            Accent,
            Race,
            Energy,
            Nothing
        }

        public String? Title { get; set; }
        public Guid ReaderUid { get; set; }
        public double? HourlyPrice { get; set; }
        public _VoiceType VoiceType { get; set; }
        public _Others Others { get; set; }
        public String? About { get; set; }
        public String? Skills { get; set; }
    }
}
