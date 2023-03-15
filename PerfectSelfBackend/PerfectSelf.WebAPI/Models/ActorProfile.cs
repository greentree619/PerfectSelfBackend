namespace PerfectSelf.WebAPI.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ActorProfile")]
    public class ActorProfile : PerfectSelfBase
    {
        public enum Vaccination_Status
        {
            Nothing
        }
        public String Title { get; set; }
        public int ActorId { get; set; }
        public String AgeRange { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public String Country { get; set; }
        public String State { get; set; }
        public String City { get; set; }
        public String AgencyCountry { get; set; }
        public Vaccination_Status VaccinationStatus { get; set; }
}
}
