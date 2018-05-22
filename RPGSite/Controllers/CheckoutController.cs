using RPGSite.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace RPGSite.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Checkout/Payment
        public ActionResult Payment()
        {
            var cart = ShoppingCart.GetCart(HttpContext);
            var itemCount = cart.GetCount();
            if (itemCount < 1)
            {
                return RedirectToAction("Index", "ShoppingCart");
            }
            ViewBag.PaymentMethodID = new SelectList(db.PaymentMethods, "ID", "Method");
            
            return View();
        }

        //POST: /Checkout/Payment
        [HttpPost]
        public ActionResult Payment(FormCollection values)
        {
            var order = new Orders();

            try
            {
                order.UserID = User.Identity.GetUserId();
                order.OrderDate = DateTime.Now;
                // Pievienot pasūtījumam apmaksas veidu 
                var pmID = 0;
                int.TryParse(values["PaymentMethodID"], out pmID);
                order.PaymentMethodID = pmID;

                // Saglabāt pasūtījumu datu bāzē
                db.Orders.Add(order);
                db.SaveChanges();

                // Saglabāt pasūtījuma preces datu bāzē
                var cart = ShoppingCart.GetCart(HttpContext);
                cart.CreateOrder(order);

                return RedirectToAction("Complete", new { id = order.ID });
            }
            catch
            {
                // Kaut kas notika nepareizi
                ViewBag.PaymentMethodID = new SelectList(db.PaymentMethods, "ID", "Method");
                return View();
            }    
        }

        public ActionResult Complete(int id)
        {
            //Validate customer owns order
            var userID = User.Identity.GetUserId();

            bool isValid = db.Orders.Any(
                o => o.ID == id &&
                o.UserID == userID);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}