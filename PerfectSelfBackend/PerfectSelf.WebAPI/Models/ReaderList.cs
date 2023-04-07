namespace PerfectSelf.WebAPI.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ReaderList
    {
        public Guid Uid { get; set; }
        public String? UserName { get; set; }
        public User.AccountType UserType { get; set; }
        public String Email { get; set; }
        public String? FirstName { get; set; }
        public String? LastName { get; set; }
        public String? Title { get; set; }
        public User.Gender Gender { get; set; }
        public Boolean IsLogin { get; set; }
        public int ReviewCount { get; set; }
        public float Score { get; set; }
        public double? HourlyPrice { get; set; }
        public DateTime? Date { get; set; }
        //public DateTime? FromTime { get; set; }
        //public DateTime? ToTime { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
