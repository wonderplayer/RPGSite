using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using RPGSite.Models;

namespace RPGSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EquipmentTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EquipmentTypes
        public ActionResult Index()
        {
            return View(db.EquipmentTypes.ToList());
        }

        // GET: EquipmentTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipmentTypes equipmentTypes = db.EquipmentTypes.Find(id);
            if (equipmentTypes == null)
            {
                return HttpNotFound();
            }
            return View(equipmentTypes);
        }

        // GET: EquipmentTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EquipmentTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Type")] EquipmentTypes equipmentTypes)
        {
            if (ModelState.IsValid)
            {
                var exists = db.EquipmentTypes.Any(e => e.Type == equipmentTypes.Type);
                if (exists)
                {
                    ViewBag.Exists = "Type already exists in the database.";
                    return View(equipmentTypes);
                }
                db.EquipmentTypes.Add(equipmentTypes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(equipmentTypes);
        }

        // GET: EquipmentTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipmentTypes equipmentTypes = db.EquipmentTypes.Find(id);
            if (equipmentTypes == null)
            {
                return HttpNotFound();
            }
            return View(equipmentTypes);
        }

        // POST: EquipmentTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Type")] EquipmentTypes equipmentTypes)
        {
            if (ModelState.IsValid)
            {
                var exists = db.EquipmentTypes.Any(e => e.Type == equipmentTypes.Type);
                if (exists)
                {
                    ViewBag.Exists = "Type already exists in the database.";
                    return View(equipmentTypes);
                }
                db.Entry(equipmentTypes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(equipmentTypes);
        }

        // GET: EquipmentTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipmentTypes equipmentTypes = db.EquipmentTypes.Find(id);
            if (equipmentTypes == null)
            {
                return HttpNotFound();
            }
            return View(equipmentTypes);
        }

        // POST: EquipmentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EquipmentTypes equipmentTypes = db.EquipmentTypes.Find(id);
            db.EquipmentTypes.Remove(equipmentTypes);
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
