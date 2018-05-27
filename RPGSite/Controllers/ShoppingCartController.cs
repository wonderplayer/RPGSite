using RPGSite.Models;
using RPGSite.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace RPGSite.Controllers
{
    // Klase priekš veikala moduļa
    public class ShoppingCartController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: ShoppingCart
        // Attēlot grozu
        // Funkcija VE.05
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(HttpContext);

            //Set up out viewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal(),
                PaymentMethods = db.PaymentMethods.ToList()
            };
            //Return the view
            return View(viewModel);
        }

        //GET: /Store/AddToCart/5
        // Pievienot grozam
        // Funkcija VE.04
        [HttpPost]
        public ActionResult AddToCart(int id)
        {
            //Retrieve the equipment from the database
            var addedEquipment = db.Equipment.Single(e => e.ID == id);

            //Add it ti the shopping cart
            var cart = ShoppingCart.GetCart(HttpContext);

            cart.AddToCart(addedEquipment);


            //Go back to the main store page for more shopping
            // return RedirectToAction("Index", new { controller = "Shop", message = true });
            return Json(new { success = true });
        }

        //AJAX: /ShoppingCart/RemoveFromCart/5
        // Iznemt preci no groza
        // Funckija VE.06
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            // Iegūt grozu
            var cart = ShoppingCart.GetCart(HttpContext);

            // Iegūt groza priekšmetus
            var cartItem = db.Carts.SingleOrDefault(item => item.RecordID == id);
            if (cartItem == null)
            {
                return Json(new { });
            }

            string equipmentName = cartItem.Equipment.Title;


            // Izņemt no groza
            int itemCount = cart.RemoveFromCart(id);

            // Attēlot ziņu
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
    }
}