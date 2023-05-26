using CsvHelper.Configuration.Attributes;

namespace PerfectSelf.WebAPI.Models
{
    public class State
    {
        public int id { get; set; }
        public string name { get; set; }
        public string country_id { get; set; }
        public string country_code { get; set; }
        public string country_name { get; set; }
        public string state_code { get; set; }
        public string type { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
    }
}
