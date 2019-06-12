using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ShoppingCart.Models;

namespace ShoppingCart.Models
{
    public class ShoppingCartDbContext : DbContext
    {
        public ShoppingCartDbContext() : base("Server=LAPTOP-E7E5F6HB; Database=shoppingcartDB; Integrated Security = True")
        {
            Database.SetInitializer(new ShoppingCartDbInitializer<ShoppingCartDbContext>());
        }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<CustomerPurchase> CustomerPurchase { get; set; }
    }

    //Drops and creates new database EVERYTIME for testing convinience
    public class ShoppingCartDbInitializer<T> :
        DropCreateDatabaseAlways<ShoppingCartDbContext>
    {
        protected override void Seed(ShoppingCartDbContext context)
        {
            //Create 4 users
            Customer c1 = new Customer();
            c1.Username = "user1";
            c1.Password = c1.HashPassword("user1");
            c1.Id = 1;
            context.Customer.Add(c1);
            Customer c2 = new Customer();
            c2.Username = "user2";
            c2.Password = c2.HashPassword("user2");
            c2.Id = 2;
            context.Customer.Add(c2);
            Customer c3 = new Customer();
            c3.Username = "user3";
            c3.Password = c3.HashPassword("user3");
            c3.Id = 3;
            context.Customer.Add(c3);
            Customer c4 = new Customer();
            c4.Username = "user4";
            c4.Password = c4.HashPassword("user4");
            c4.Id = 4;
            context.Customer.Add(c4);

            //Create Products as shown in the CA requirements
            Product p1 = new Product();
            p1.Id = 1;
            p1.Name = ".NET Charts";
            p1.Price = 99;
            p1.Description = "Brings powerful charting capabilities to your .Net applications.";
            p1.Img = "/Image/chart1.jpg";
            context.Product.Add(p1);
            Product p2 = new Product();
            p2.Id = 2;
            p2.Name = ".NET PayPal";
            p2.Price = 69;
            p2.Description = "Integrate your .NET apps with PayPal the easy way!";
            p2.Img = "/Image/Paypal1.jpg";
            context.Product.Add(p2);
            Product p3 = new Product();
            p3.Id = 3;
            p3.Name = ".NET ML";
            p3.Price = 299;
            p3.Description = "Supercharged .NET machine learning libraries";
            p3.Img = "/Image/ML.jpeg";
            context.Product.Add(p3);
            Product p4 = new Product();
            p4.Id = 4;
            p4.Name = ".NET Analytics";
            p4.Price = 299;
            p4.Description = "Performs data mining and analytics easily in .NET";
            p4.Img = "/Image/analytics1.jpg";
            context.Product.Add(p4);
            Product p5 = new Product();
            p5.Id = 5;
            p5.Name = ".NET Logger";
            p5.Price = 49;
            p5.Description = "Logs and aggregates events easily in your .NET apps";
            p5.Img = "/Image/logger1.jpg";
            context.Product.Add(p5);
            Product p6 = new Product();
            p6.Id = 6;
            p6.Name = ".NET Numerics";
            p6.Price = 199;
            p6.Description = "Powerful numerical methods for your .NET simulations";
            p6.Img = "/Image/numerics1.jpg";
            context.Product.Add(p6);
            base.Seed(context);
        }
    }
}