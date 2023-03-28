namespace PerfectSelf.WebAPI.Models
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ReaderProfile")]
    [Index(nameof(ReaderUid), IsUnique = true)]
    public class ReaderProfile : PerfectSelfBase
    {
        public enum _VoiceType
        {
            DeepVoice,
            RaspyVoice,
            HighVoice,
            Nothing=-1
        }

        public enum _Others
        {
            Archetype,
            Accent,
            Race,
            Energy,
            Nothing = -1
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
