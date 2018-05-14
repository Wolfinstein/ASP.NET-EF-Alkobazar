namespace Alkobazar.Migrations.Identity
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Alkobazar.Models;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    internal sealed class Configuration : DbMigrationsConfiguration<Alkobazar.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\Identity";
        }

        protected override void Seed(Alkobazar.Models.ApplicationDbContext context)
        {
            ApplicationDbContext db = new ApplicationDbContext();


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
            if (userManager.FindByEmail("employee@gmail.com") == null)
            {
                var user = new ApplicationUser
                {
                    FirstName = "Employee",
                    LastName = "Employee",
                    Email = "employee@gmail.com",
                    UserName = "employee@gmail.com",

                };
                var result = userManager.Create(user, "123456");
                if (result.Succeeded)
                    userManager.AddToRole(userManager.FindByEmail(user.Email).Id, "Employee");
                }

            var customer = new Customer
            {
                Name = "Biedronka",
                Phone = "123321",
                Shipment_Address = "Azaliowa 21",
            };
            db.Customers.Add(customer);

            customer = new Customer
            {
                Name = "Lidl",
                Phone = "12321321",
                Shipment_Address = "Bazaliowa 32",
            };
            db.Customers.Add(customer);


            var product = new Product
            {
                Name = "Kasztelan",
                Description = "none",
                Alcohol_content = 0.05,
                Price = 10,
                QuantityInStock = 100000,
                SizeInLiters = 0.5
            };
            db.Product.Add(product);

            product = new Product
            {
                Name = "Perla",
                Description = "none",
                Alcohol_content = 0.06,
                Price = 5,
                QuantityInStock = 25000,
                SizeInLiters = 0.5
            };
            db.Product.Add(product);

            db.SaveChanges();


            var order = new Order
            {
                Create_timestamp = DateTime.Now,
                Customer = db.Customers.Where(a => a.Name == "Biedronka").First(),
                Deadline = DateTime.Now,
                User = db.Users.Where(u => u.Email == "employee@gmail.com").First(),
                Order_Number = "#1"
            };
            db.Orders.Add(order);

            order = new Order
            {
                Create_timestamp = DateTime.Now,
                Customer = db.Customers.Where(a => a.Name == "Lidl").First(),
                Deadline = DateTime.Now,
                User = db.Users.Where(u => u.Email == "employee@gmail.com").First(),
                Order_Number = "#2"
            };
            db.Orders.Add(order);

            db.SaveChanges();


        }
    }
}