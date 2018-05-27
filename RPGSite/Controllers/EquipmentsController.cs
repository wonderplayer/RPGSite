using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using RPGSite.Models;
using System.Web;
using System.IO;
using RPGSite.ViewModels;
using PagedList;

namespace RPGSite.Controllers
{
    // Klase realizē ekipējumu moduli
    [Authorize(Roles = "Admin")]
    public class EquipmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Equipments
        // Attēlot visus ekipējumus ar to kārtošanu un meklēšanu
        // Funckcija EK.05, EK.08, EK.09
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            // Pārbaudīt, vai tiek kārtots pēc kāda parametra
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParam = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.RaritySortParam = sortOrder == "Rarity" ? "rarity_desc" : "Rarity";
            ViewBag.TypeSortParam = sortOrder == "Type" ? "type_desc" : "Type";

            // pĀrbaudīt, vai tika kaut kas meklēts
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
            // Sakārtot pēc izvēlētās kolonnas
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

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(equipment.ToPagedList(pageNumber, pageSize));
        }

        // GET: Equipments/Details/5
        // Apskatīties ekipējuma detaļas
        // Funkcija EK.01
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipment equipment = db.Equipment.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            return View(equipment);
        }

        // GET: Equipments/Create
        // Ekipējuma izveidošanas skats
        public ActionResult Create()
        {
            ViewBag.RarityID = new SelectList(db.EquipmentRarities, "ID", "Rarity");
            ViewBag.TypeID = new SelectList(db.EquipmentTypes, "ID", "Type");
            return View();
        }

        // POST: Equipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // Izveidot ekipējumu
        // Funkcija EK.03
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Description,Price,Picture,TypeID,RarityID")] Equipment equipment, HttpPostedFileBase Picture)
        {
            if (ModelState.IsValid)
            {
                // Pārbaudīt, vai bilde atbilst noteikumiem
                if (Picture != null && Picture.ContentLength > 0)
                {
                    var picture = Path.GetFileName(Picture.FileName);
                    var extenstion = Path.GetExtension(Picture.FileName);
                    if (extenstion != ".jpg" && extenstion != ".png" && extenstion != ".jpeg" && extenstion != ".gif")
                    {
                        ViewBag.PictureError = "The picture should be format of .jpg, .gif, .png or .jpeg";
                        ViewBag.RarityID = new SelectList(db.EquipmentRarities, "ID", "Rarity", equipment.RarityID);
                        ViewBag.TypeID = new SelectList(db.EquipmentTypes, "ID", "Type", equipment.TypeID);
                        return View(equipment);
                    }
                    var folder = db.EquipmentTypes.Find(equipment.TypeID).Type.ToString();
                    var databasePath = folder + "/" + picture;
                    equipment.Picture = databasePath;                  
                    var path = Path.Combine(Server.MapPath("~/images/"), databasePath);
                    var folderPath = Path.Combine(Server.MapPath("~/images/"), folder);
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    Picture.SaveAs(path);
                }

                db.Equipment.Add(equipment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RarityID = new SelectList(db.EquipmentRarities, "ID", "Rarity", equipment.RarityID);
            ViewBag.TypeID = new SelectList(db.EquipmentTypes, "ID", "Type", equipment.TypeID);
            return View(equipment);
        }

        // GET: Equipments/Edit/5
        // Atvērt ekipējuma rediģēšanas skatu
        // Funkcija EK.06
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipment equipment = db.Equipment.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            ViewBag.RarityID = new SelectList(db.EquipmentRarities, "ID", "Rarity", equipment.RarityID);
            ViewBag.TypeID = new SelectList(db.EquipmentTypes, "ID", "Type", equipment.TypeID);
            ViewBag.PicturePath = Path.Combine(Server.MapPath("~/images/"), equipment.Picture);
            // Iegūst datus rediģēšanai
            EditEquipmentViewModel editEquipment = new EditEquipmentViewModel
            {
                ID = equipment.ID,
                Title = equipment.Title,
                Description = equipment.Description,
                Picture = equipment.Picture,
                Price = equipment.Price,
                RarityID = equipment.RarityID,
                TypeID = equipment.TypeID,
                Rarity = equipment.Rarity,
                Type = equipment.Type
            };
            return View(editEquipment);
        }

        // POST: Equipments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // Rediģēt ekipējuma datus
        // Funkcija EK.04
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Description,Price,Picture,TypeID,RarityID")] EditEquipmentViewModel editedEquipment, HttpPostedFileBase Picture, string currentPicturePath)
        {
            if (Picture == null)
            {
                editedEquipment.Picture = currentPicturePath;
            }
            else
            {
                var picture = Path.GetFileName(Picture.FileName);
                var folder = db.EquipmentTypes.Find(editedEquipment.TypeID).Type.ToString();
                var databasePath = folder + "/" + picture;
                editedEquipment.Picture = databasePath;
                var path = Path.Combine(Server.MapPath("~/images/"), databasePath);
                Picture.SaveAs(path);
            }
            if (ModelState.IsValid)
            {
                Equipment equipment = new Equipment
                {
                    ID = editedEquipment.ID,
                    Title = editedEquipment.Title,
                    Description = editedEquipment.Description,
                    RarityID = editedEquipment.RarityID,
                    TypeID = editedEquipment.TypeID,
                    Picture = editedEquipment.Picture,
                    Price = editedEquipment.Price,
                    Rarity = editedEquipment.Rarity,
                    Type = editedEquipment.Type
                };
                db.Entry(equipment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RarityID = new SelectList(db.EquipmentRarities, "ID", "Rarity", editedEquipment.RarityID);
            ViewBag.TypeID = new SelectList(db.EquipmentTypes, "ID", "Type", editedEquipment.TypeID);
            return View(editedEquipment);
        }

        // GET: Equipments/Delete/5
        // Attēlot dzēšanas skatu
        // Funkcija EK.07
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipment equipment = db.Equipment.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            return View(equipment);
        }

        // POST: Equipments/Delete/5
        // Dzēst ekipējumu
        // Funkcija EK.02
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Equipment equipment = db.Equipment.Find(id);
            db.Equipment.Remove(equipment);
            db.SaveChanges();
            return RedirectToAction("Index");
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
