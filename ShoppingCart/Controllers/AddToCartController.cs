using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCart.Models;
using ShoppingCart.Utilities;

namespace ShoppingCart.Controllers
{
    public class AddToCartController : Controller
    {
        // GET: AddToCart



        // GET: AddToCart  
        public ActionResult Add(string id, string cmd)//get product id as Id
        {
            Product mo = Util.GetProductById(int.Parse(id));//get product based on id using the utility model
            
            if (Session["cart"] == null)//create a session["cart"] check whether it is null
            {
                List<Product> li = new List<Product>();                
                li.Add(mo);
                Session["cart"] = li; //Add products to cart
                ViewBag.cart = li.Count();
                Session["count"] = 1;


            }
            //add products to existing session["cart"]
            else if(cmd != "minus")
            {
                List<Product> li = (List<Product>)Session["cart"];
                li.Add(mo);
                Session["cart"] = li;
                ViewBag.cart = li.Count();
                Session["count"] = Convert.ToInt32(Session["count"]) + 1;

            }

            if (cmd == "minus")
            {
                List<Product> li = (List<Product>)Session["cart"];
                int itemPosition = li.FindIndex(x => x.Id == mo.Id);
                li.RemoveAt(itemPosition);
                Session["cart"] = li;
                ViewBag.cart = li.Count();
                Session["count"] = Convert.ToInt32(Session["count"]) - 1;
            }
            //return view based on cmd 
            if (cmd == "add" || cmd == "minus")            
                return RedirectToAction("Myorder");
            else
                return RedirectToAction("Gallery","Product");          

        }
        public ActionResult Myorder()
        {
            List<Product> ProductList = (List<Product>)Session["cart"] ; //add session["cart"] to List

            return View(ProductList);// return List of products

        }
    }
}