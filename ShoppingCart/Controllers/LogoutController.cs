using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCart.Controllers;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers
{
    public class LogoutController : Controller
    {
        // GET: Logout
        
        public ActionResult Logout()
        {
            RemoveSession(); 
            return RedirectToAction("Login", "Login");

        }
        public void RemoveSession()
        {
            //Acess database and enter session into database
            using (var db = new ShoppingCartDbContext())
            {
                string SessionId = (string)Session["SessionId"];
                Customer customer = db.Customer.Where(x => x.SessionId == SessionId).FirstOrDefault();

                customer.SessionId = null;
                Session["cart"] = null;
                Session["count"] = 0;
                //save changes
                db.SaveChanges();

            }

        }
    }
}