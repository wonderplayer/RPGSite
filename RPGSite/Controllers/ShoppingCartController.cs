using RPGSite.Models;
using RPGSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RPGSite.Controllers
{
    public class ShoppingCartController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(HttpContext);

            //Set up out viewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            //Return the view
            return View(viewModel);
        }

        //GET: /Store/AddToCart/5
        public ActionResult AddToCart(int id)
        {
            //Retrieve the equipment from the database
            var addedEquipment = db.Equipment.Single(e => e.ID == id);

            //Add it ti the shopping cart
            var cart = ShoppingCart.GetCart(HttpContext);

            cart.AddToCart(addedEquipment);

            //Go back to the main store page for more shopping
            return RedirectToAction("Index", new { controller = "Shop"});
        }

        //AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            //Remove item from the cart
            var cart = ShoppingCart.GetCart(HttpContext);

            //Get the name of the equipment to display confirmation
            string equipmentName = db.Carts.Single(item => item.RecordID == id).Equipment.Title;

            //Remove from cart
            int itemCount = cart.RemoveFromCart(id);

            //Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(equipmentName) +
                    " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };

            return Json(results);
        }

        //GET: /ShoppingCart/CartSummary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }
    }
}