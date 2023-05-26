using CsvHelper.Configuration.Attributes;

namespace PerfectSelf.WebAPI.Models
{
    public class Country
    {
        //[Index(0)]
        public int id { get; set; }
        //[Index(1)]
        public string name { get; set; }
        //[Index(2)]
        public string iso3 { get; set; }
        //[Index(3)]
        public string iso2 { get; set; }
        //[Index(4)]
        public string numeric_code { get; set; }
        //[Index(5)]
        public string phone_code { get; set; }
        //[Index(6)]
        public string capital { get; set; }
        //[Index(7)]
        public string currency { get; set; }
        //[Index(8)]
        public string currency_name { get; set; }
        //[Index(9)]
        public string currency_symbol { get; set; }
        //[Index(10)]
        public string tld { get; set; }
        //[Index(11)]
        public string native { get; set; }
        //[Index(12)]
        public string region { get; set; }
        //[Index(13)]
        public string subregion { get; set; }
        //[Index(14)]
        public string timezones { get; set; }
        //[Index(15)]
        public string latitude { get; set; }
        //[Index(16)]
        public string longitude { get; set; }
        //[Index(17)]
        public string emoji { get; set; }
        //[Index(18)]
        public string emojiU { get; set; }
    }
}
