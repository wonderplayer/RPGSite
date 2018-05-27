using RPGSite.Models;
using System.Collections.Generic;

namespace RPGSite.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
        public List<PaymentMethods> PaymentMethods { get; set; }
    }
}