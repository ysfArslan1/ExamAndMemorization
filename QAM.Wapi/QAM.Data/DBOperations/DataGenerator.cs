
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QAM.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QAM.Data.DBOperations
{
    public class DataGenerator
    {
        //Database de data üretmek içinkullanılıyor
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var content = new QmDbContext(serviceProvider.GetRequiredService<DbContextOptions<QmDbContext>>()))
            {
            if (!content.Roles.Any())
                {
                    content.Roles.AddRange(
                     new Role
                    {
                        Name = "Admin"
                    },
                    new Role
                    {
                        Name = "Client"
                    }
                    );
                    content.SaveChanges();
                }
                if (!content.Users.Any())
                {
                    content.Users.AddRange(
                    new User
                    {
                        IdentityNumber = "11111111111",
                        FirstName = "admin",
                        LastName = "admin",
                        Email = "a@gmail.com",
                        Password = "12345",
                        DateOfBirth = DateTime.Now.AddYears(-23),
                        LastActivityDate = DateTime.Now.AddDays(-2),
                        RoleId=1
                    },
                    new User
                    {
                        IdentityNumber = "11111111112",
                        FirstName = "employee",
                        LastName = "employee",
                        Email = "e@gmail.com",
                        Password = "12345",
                        DateOfBirth = DateTime.Now.AddYears(-23),
                        LastActivityDate = DateTime.Now.AddDays(-2),
                        RoleId = 2
                    }
                    );
                    content.SaveChanges();
                }


            }
        }
    }
}
