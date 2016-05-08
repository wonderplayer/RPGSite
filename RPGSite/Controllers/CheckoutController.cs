using RPGSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                var pmID = 0;
                int.TryParse(values["PaymentMethodID"], out pmID);
                order.PaymentMethodID = pmID;

                //SaveOrder
                db.Orders.Add(order);
                db.SaveChanges();
                //Process the order
                var cart = ShoppingCart.GetCart(HttpContext);
                cart.CreateOrder(order);

                return RedirectToAction("Complete", new { id = order.ID });
            }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
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