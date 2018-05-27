using Microsoft.AspNet.Identity;
using RPGSite.Models;
using System.Linq;
using System.Web.Mvc;
using RPGSite.ViewModels;

namespace RPGSite.Controllers
{
    [Authorize]
    public class InventoryController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Inventory
        // Attēlo lietotāja inventāru
        // Funkcija LI.08
        public ActionResult Index()
        {
            var userID = User.Identity.GetUserId();
            var inventory = new InventoryViewModel
            {
                Inventory = db.Inventories.Where(i => i.UserID == userID).ToList()
            };
            return View(inventory);
        }
    }
}