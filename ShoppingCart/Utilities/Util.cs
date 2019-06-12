using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShoppingCart.Models;

namespace ShoppingCart.Utilities
{
    public class Util
    {
        //Method to get Product by Id
        public static Product GetProductById(int Id)
        {
            using (var db = new ShoppingCartDbContext())
            {
                Product product = db.Product.Where(x => x.Id == Id).FirstOrDefault();
                return product;
            }
        }
    }
}