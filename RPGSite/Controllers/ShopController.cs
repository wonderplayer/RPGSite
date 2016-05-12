using RPGSite.Models;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using PagedList;

namespace RPGSite.Controllers
{
    public class ShopController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Equipments
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParam = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.RaritySortParam = sortOrder == "Rarity" ? "rarity_desc" : "Rarity";
            ViewBag.TypeSortParam = sortOrder == "Type" ? "type_desc" : "Type";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var equipment = db.Equipment.Include(e => e.Rarity).Include(e => e.Type);
            if (!string.IsNullOrEmpty(searchString))
            {
                equipment = equipment.Where(e => e.Title.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "title_desc":
                    equipment = equipment.OrderByDescending(e => e.Title);
                    break;
                case "Rarity":
                    equipment = equipment.OrderBy(e => e.Rarity.Rarity);
                    break;
                case "rarity_desc":
                    equipment = equipment.OrderByDescending(e => e.Rarity.Rarity);
                    break;
                case "Type":
                    equipment = equipment.OrderBy(e => e.Type.Type);
                    break;
                case "type_desc":
                    equipment = equipment.OrderByDescending(e => e.Type.Type);
                    break;
                default:
                    equipment = equipment.OrderBy(e => e.Title);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(equipment.ToPagedList(pageNumber, pageSize));
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
