using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;
using ShoppingCart.Models;
using System.Data.Entity;

namespace ShoppingCart.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(string Username, string Password)
        {
            //if brought to this page without username or password input, login.
            if (Username == null)
            {
                ViewData["WrongPassword"] = false;
                return View();
            }
            //if not, check if password is correct. If yes, succssfully login and create a session for customer
            else
            {
                //If password don't match username in database, incorrect, throw to login again.
                if (!PasswordIsRight(Username, Password))
                {
                    ViewData["WrongPassword"] = true;
                    return View();
                }
                //if successfully login, create sessionId for customer and send to shop.
                else
                {
                    string SessionId = CreateSession(Username);
                    Session["SessionId"] = SessionId;
                    return RedirectToAction("Gallery", "Product", new { Username, SessionId });
                }
            }

        }
        //Generate hashed password
        public string HashGenerator(string password)
        {
            //HashedPassword using MD5 hash. Note: need to hash the byte of the password
            MD5 md5 = new MD5CryptoServiceProvider();
            Byte[] originalBytes = ASCIIEncoding.Default.GetBytes(password);
            Byte[] encodedBytes = md5.ComputeHash(originalBytes);
            //return as a hash string
            return BitConverter.ToString(encodedBytes);
        }

        //Checking sessions
        public bool SessionExist(string sessionId)
        {
            using (var db = new ShoppingCartDbContext())
            {
                List<Customer> customers = db.Customer.Where(x => x.SessionId.Contains(sessionId)).ToList();
                //Session exists if there is something in the list (its in database)
                if (customers.Count > 0)
                {
                    return true;
                }
                else
                    return false;
            }
        }

        //Check whether hashed Password is correct and return true if tallies
        public bool PasswordIsRight(string Username, string password)
        {
            //Hash the password
            string hashedPassword = HashGenerator(password);

            using (var db = new ShoppingCartDbContext())
            {
                //fetch data of customers
                List<Customer> customers = db.Customer.Where(x => x.Username.Contains(Username)).ToList();
                //Check if there is Username, and check if password is same.
                //If match, return true.
                if(customers.Count == 1)
                {
                    if(customers.ElementAt(0).Password == hashedPassword)
                    {
                        return true;
                    }
                }
                //If exit from check, no matching username and password. Return false.
                return false;
            }
        }

        //Create Session for customer and returns the sessionId to be used.
        //Note: Assumes Username exists (must login successfully first)
        public string CreateSession(string Username)
        {
            //Acess database and enter session into database
            using (var db = new ShoppingCartDbContext())
            {
                //Add sessionId to customer table for customer who login
                string sessionId = Guid.NewGuid().ToString();
                Customer customer = db.Customer.Where(x => x.Username == Username).FirstOrDefault();
                customer.SessionId = sessionId;
                //save changes
                db.SaveChanges();

                return sessionId;
            }
        }
    }
}