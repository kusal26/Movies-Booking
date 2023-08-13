using DotNetProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetProject
{
    public class AdminCredSeed
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope=applicationBuilder.ApplicationServices.CreateScope())
            {
              var context=  serviceScope.ServiceProvider.GetService<DotNetProjectContext>();
                context.Database.EnsureCreated();
                if(!context.adminCreds.Any())
                {
                    var admincred=new AdminCred()
                    {
                        Email="admin@gmail.com",
                        Password="admin123"
                    };
                    context.adminCreds.Add(admincred);
                    context.SaveChanges();
                }
                
            }
        }
    }
}
