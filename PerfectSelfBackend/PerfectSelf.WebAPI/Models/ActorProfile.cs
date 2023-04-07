namespace PerfectSelf.WebAPI.Models
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ActorProfile")]
    [Index(nameof(ActorUid), IsUnique = true)]
    public class ActorProfile : PerfectSelfBase
    {
        public enum Vaccination_Status
        {
            Nothing
        }
        public String Title { get; set; }
        public Guid ActorUid { get; set; }
        public String AgeRange { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public String Country { get; set; }
        public String State { get; set; }
        public String City { get; set; }
        public String AgencyCountry { get; set; }
        public int ReviewCount { get; set; } = 0;
        public float Score { get; set; } = 0.0f;
        public Vaccination_Status VaccinationStatus { get; set; }
}
}
