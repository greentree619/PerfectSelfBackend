using CsvHelper.Configuration.Attributes;

namespace PerfectSelf.WebAPI.Models
{
    public class City
    {
        public int id { get; set; }
        public string name { get; set; }
        public string state_id { get; set; }
        public string state_code { get; set; }
        public string state_name { get; set; }
        public string country_id { get; set; }
        public string country_code { get; set; }
        public string country_name { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string wikiDataId { get; set; }
    }
}
