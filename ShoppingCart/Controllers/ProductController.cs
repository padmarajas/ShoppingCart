using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCart.Models;
using System.Data;
using System.Diagnostics;



namespace ShoppingCart.Controllers
{
    public class ProductController : Controller
    {
        // GET: Shop
        
        //Get search string from View for searching products
        public ActionResult Gallery(string search)
        {
            List<Product> product = new List<Product>();          
            
            using (var db = new ShoppingCartDbContext())
            {
                string SessionId = (string)Session["SessionId"];
                Customer customer = db.Customer.Where(x => x.SessionId == SessionId).FirstOrDefault();
                string username = customer.Username;
                product = (from p in db.Product select p).ToList();
                // select products from Db based on search string.
                if (!string.IsNullOrEmpty(search))
                {
                    product = (from p in db.Product where p.Name.Contains(search)  select p).ToList();
                    
                }
                // add products to list and return to view through ViewData
                ViewData["Products"] = product;
                ViewData["Username"] = username;
                return View();
            }
        }
       
    }
}