namespace PerfectSelf.WebAPI.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("User")]
    public class User: PerfectSelfBase
    {
        public enum AccountType
        {
            Actor,
            Reader,
            Nothing
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public AccountType UserType { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string CurrentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string City { get; set; }
        public string Nationality { get; set; }
        public string PhoneNumber { get; set; }
    }
}
