namespace Alkobazar.Migrations.Identity
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Alkobazar.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Alkobazar.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\Identity";
        }

        protected override void Seed(Alkobazar.Models.ApplicationDbContext context)
        {
                      

                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                if (!roleManager.RoleExists("Admin"))
                    roleManager.Create(new IdentityRole("Admin"));
                if (!roleManager.RoleExists("Employee"))
                    roleManager.Create(new IdentityRole("Employee"));
          
         
             var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
             if (userManager.FindByEmail("admin@gmail.com") == null)
             {
                 var user = new ApplicationUser
                 {
                     FirstName = "Admin" ,
                     LastName = "Admin",
                     Email = "admin@gmail.com",
                     UserName = "admin@gmail.com",
                     
                 };
                 var result = userManager.Create(user, "123456");
                 if (result.Succeeded)
                     userManager.AddToRole(userManager.FindByEmail(user.Email).Id, "Admin");

             }

        }
    }
}