using RPGSite.Models;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using PagedList;

namespace RPGSite.Controllers
{
    // Klase priekš veikala moduļa
    public class ShopController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Equipments
        // Attēlot veikalu ar meklēšanu un kārtošanu
        // Funkcijas VE.01, VE.02, VE.03
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, bool? message)
        {
            // Pārbauda, vai tiek kārtots pēc kādas kolonnas
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParam = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.RaritySortParam = sortOrder == "Rarity" ? "rarity_desc" : "Rarity";
            ViewBag.TypeSortParam = sortOrder == "Type" ? "type_desc" : "Type";

            // Pārbauda, vai priekšmets tika pievienots grozam
            if (message != null && message == true)
            {
                ViewBag.Message = "Item has been added to your cart";
            }

            // Pārbauda, vai tika kaut kas meklēts
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
            // Pārbauda pēc kā tiek kārtots
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

            int pageSize = 5;
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
