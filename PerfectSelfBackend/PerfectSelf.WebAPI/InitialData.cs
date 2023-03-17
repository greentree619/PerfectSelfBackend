namespace PerfectSelf.WebAPI
{
    using System.Linq;
    using PerfectSelf.WebAPI.Context;
    using PerfectSelf.WebAPI.Models;

    public static class InitialData
    {
        public static void Seed(this PerfectSelfContext dbContext)
        {
            if (!dbContext.Users.Any())
            {
                dbContext.Users.Add(new User
                {
                    UserType = User.AccountType.Admin,
                    UserName = "Admin",
                    Email = "admin@gmail.com",
                    Password = "123456",
                    FirstName = "First Name",
                    LastName = "Last Name",
                    Gender = PerfectSelfBase.Gender.Male,
                    DateOfBirth = "03-16-2023",
                    Nationality = "USA",
                    City = "City",
                    CurrentAddress = "Current Address",
                    PermanentAddress = "Permanent Address",
                    PhoneNumber = "1234567890",
                    IsLogin = false,
                    Token = "",
                });
                dbContext.Users.Add(new User
                {
                    Email = "tester@gmail.com",
                    Password = "123456",
                    FirstName = "First Name",
                    LastName = "Last Name",
                    UserName = "tester",
                    UserType = User.AccountType.Developer,
                    Gender = PerfectSelfBase.Gender.Male,
                    DateOfBirth = "03-16-2023",
                    Nationality = "USA",
                    City = "City",
                    CurrentAddress = "Current Address",
                    PermanentAddress = "Permanent Address",
                    PhoneNumber = "1234567890",
                    IsLogin = false,
                    Token = "",
                });
                dbContext.SaveChanges();
            }
        }
    }
}
