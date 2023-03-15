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
                    UserName = "Admin",
                    Gender = PerfectSelfBase.Gender.Male,
                    DateOfBirth = "03-16-2023",
                    Nationality = "USA",
                    City = "City",
                    CurrentAddress = "Current Address",
                    PermanentAddress = "Permanent Address"
                });
                dbContext.Users.Add(new User
                {
                    UserName = "tester",
                    Gender = PerfectSelfBase.Gender.Male,
                    DateOfBirth = "03-16-2023",
                    Nationality = "USA",
                    City = "City",
                    CurrentAddress = "Current Address",
                    PermanentAddress = "Permanent Address"
                });
                dbContext.SaveChanges();
            }
        }
    }
}
