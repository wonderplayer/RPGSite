using RPGSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPGSite.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
        public List<PaymentMethods> PaymentMethods { get; set; }
    }
}