using System;
using System.Data;
using System.Linq;
using Core.Security;
using Entity.Customers;
using Entity.SystemUsers;
using static Core.Enums.Enums;

namespace DAL
{
    public static class DataSeeder
    {
        public static void Initialize(ApplicationContext context)
        {
            var cry = new Cryptography();
            string password = cry.EncryptString("123456");
            if (!context.Set<User>().Any(x => x.Role == Role.Admin))
            {
                context.Set<User>().Add(new User
                {
                    Role = Role.Admin,
                    FirstName = "Admin",
                    LastName = "Admin",
                    Email = "admin@admin.com",
                    Password = password,
                    Username = "admin"
                });
                context.SaveChanges();
            }
            if (!context.Set<Customer>().Any())
            {
                context.Set<Customer>().Add(new Customer
                {
                    Name = "cihan",
                    Surname = "oguz",
                    BirthDate = new DateTime(),
                    Gender = Gender.Male,
                    IdentityNo = "11111111111"
                });
                context.SaveChanges();
            }
        }
    }
}

