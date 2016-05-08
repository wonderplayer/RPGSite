using RPGSite.Models;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;

namespace RPGSite.Controllers
{
    public class ShopController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Equipments
        public ActionResult Index()
        {
            var equipment = db.Equipment.Include(e => e.Rarity).Include(e => e.Type);
            return View(equipment.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
