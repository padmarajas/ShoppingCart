using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        public ActionResult Check()
        {
            List<Product> purchases = (List<Product>)Session["cart"]; // add session["cart"] products to list
            using (var db = new ShoppingCartDbContext())
            {
                if (Session["SessionId"] != null)//check if sessionId is null
                {
                    string SessionId = (string)Session["SessionId"];
                    Customer customer = db.Customer.Where(x => x.SessionId == SessionId).FirstOrDefault(); // retrieve customer based on SessionId

                    double currentTime = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                    DateTime dateTimeNow = new DateTime(1970, 1, 1);
                    dateTimeNow = dateTimeNow.AddSeconds(currentTime);
                    string dateNow = dateTimeNow.ToString("dd MMMM yyyy"); //convert datetime to string (from seconds)
                    foreach (var purchase in purchases)
                    {
                        CustomerPurchase cp = new CustomerPurchase();
                        cp.CustomerId = customer.Id;
                        cp.ProductId = purchase.Id;
                        cp.ActivationCode = Guid.NewGuid().ToString();
                        cp.DatePurchased = dateNow;
                        cp.Customer = customer;
                        cp.Product = purchase;
                        db.CustomerPurchase.Add(cp); // add customer puchases to DB
                    }
                    db.SaveChanges();//Save changes
                }
                Session["cart"] = null; // after successful purchase empty cart
                Session["count"] = 0; // empty count of cart
               return RedirectToAction("Mypurchases");
            }
        }

        public ActionResult Mypurchases()
        {
            using(var db = new ShoppingCartDbContext())
            {
                string sessionId = (string)Session["SessionId"];
                //select purchases based on customer id and session id from DB
                var mypurchases = (from cp in db.CustomerPurchase
                                   join customer in db.Customer
                                   on cp.CustomerId equals customer.Id
                                   join product in db.Product
                                   on cp.ProductId equals product.Id
                                   where customer.SessionId == sessionId
                                   select new {
                                       cp.ActivationCode,
                                       cp.DatePurchased,
                                       product.Name,
                                       product.Description,
                                       product.Img
                                   });
                List<MyPurchaseModel> PmList = new List<MyPurchaseModel>();
                foreach(var purchase in mypurchases)
                {
                    MyPurchaseModel pm = new MyPurchaseModel();
                    pm.ActivationCode = purchase.ActivationCode;
                    pm.DateOfPurchase = purchase.DatePurchased;
                    pm.Name = purchase.Name;
                    pm.Description = purchase.Description;
                    pm.Image = purchase.Img;
                    PmList.Add(pm);
                    
                } // add List to MypurchaseModel
                ViewData["mypurchases"] = PmList; ;
                return View();
            }
            
           
        }
    }

}