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
        public ActionResult Index()
        {
            var userID = User.Identity.GetUserId();
            var inventory = new InventoryViewModel
            {
                Inventory = db.Inventories.Where(i => i.UserID == userID).ToList()
            };
            //var equipment = db.Equipment.Include(e => e.Rarity).Include(e => e.Type);
            return View(inventory);
        }
    }
}