using static PerfectSelf.WebAPI.Models.User;

namespace PerfectSelf.WebAPI.Models
{
    public class ReaderDetailProfile : ReaderProfile
    {
        public Guid Uid { get; set; }
        public AccountType UserType { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string CurrentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string City { get; set; }
        public string Nationality { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsLogin { get; set; }
        public string Token { get; set; }
    }
}
