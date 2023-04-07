namespace PerfectSelf.WebAPI.Models
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("User")]
    [Index(nameof(Email), IsUnique = true)]
    public class User: PerfectSelfBase
    {
        public enum AccountType
        {
            /*0*/Admin,
            /*1*/Developer,
            /*2*/Tester,
            /*3*/Actor,
            /*4*/Reader,
            Nothing = -1
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Uid { get; set; } = Guid.NewGuid();
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
        public bool IsLogin { get; set; } = false;
        public string Token { get; set; }
    }
}
