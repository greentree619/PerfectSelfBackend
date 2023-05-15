namespace PerfectSelf.WebAPI.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class PerfectSelfBase
    {
        public enum Gender
        {
            Male,
            Female,
            Decline,
            Nothing
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedTime { get; set; } = DateTime.UtcNow;
        public DateTime DeletedTime { get; set; }
    }
}
