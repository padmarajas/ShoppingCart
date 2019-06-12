using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{

    //use this model to view purchases of customer
    public class MyPurchaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ActivationCode { get; set; }
        public string DateOfPurchase { get; set; }
        public string Image { get; set; }
    }
}