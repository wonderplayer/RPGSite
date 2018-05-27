using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using RPGSite.Models;

namespace RPGSite.Controllers
{
    // Klase apraksta ekipējuma retumu moduli
    [Authorize(Roles = "Admin")]
    public class EquipmentRaritiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EquipmentRarities
        // Attēlot ekipējuma retumus
        // Funkcija ER.05
        public ActionResult Index()
        {
            return View(db.EquipmentRarities.ToList());
        }

        // GET: EquipmentRarities/Details/5
        // Attēlot ekipējuma retumu
        // Funkcija ER.01
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipmentRarities equipmentRarities = db.EquipmentRarities.Find(id);
            if (equipmentRarities == null)
            {
                return HttpNotFound();
            }
            return View(equipmentRarities);
        }

        // GET: EquipmentRarities/Create
        // Attēlot ekipējuma retuma izveidošanas skatu
        public ActionResult Create()
        {
            return View();
        }

        // POST: EquipmentRarities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // Pievienot ekipējuma retumu datu bāzē
        // Funkcija ER.03
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Rarity")] EquipmentRarities equipmentRarities)
        {
            ViewBag.Exists = "";
            if (ModelState.IsValid)
            {
                // Pārbauda, vai jau eksistē tāds datu bāzē
                var exists = db.EquipmentRarities.Any(e => e.Rarity == equipmentRarities.Rarity);
                if (exists)
                {
                    ViewBag.Exists = "Rarity already exists in the database.";
                    return View(equipmentRarities);
                }
                db.EquipmentRarities.Add(equipmentRarities);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(equipmentRarities);
        }

        // GET: EquipmentRarities/Edit/5
        // Attēlot ekipējuma retuma rediģēšanas skatu
        // Funkcija ER.06
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipmentRarities equipmentRarities = db.EquipmentRarities.Find(id);
            if (equipmentRarities == null)
            {
                return HttpNotFound();
            }
            return View(equipmentRarities);
        }

        // POST: EquipmentRarities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // Rediģēt ekipējuma retumu
        // Funkcija ER.04
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Rarity")] EquipmentRarities equipmentRarities)
        {
            if (ModelState.IsValid)
            {
                var exists = db.EquipmentRarities.Any(e => e.Rarity == equipmentRarities.Rarity);
                if (exists)
                {
                    ViewBag.Exists = "Rarity already exists in the database.";
                    return View(equipmentRarities);
                }
                // Atrod un rediģē ierakstu datu bāzē ar jaunajiem datiem
                db.Entry(equipmentRarities).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(equipmentRarities);
        }

        // GET: EquipmentRarities/Delete/5
        // Atvērt dzēšanas skatu
        // Funkcija ER.07
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipmentRarities equipmentRarities = db.EquipmentRarities.Find(id);
            if (equipmentRarities == null)
            {
                return HttpNotFound();
            }
            return View(equipmentRarities);
        }

        // POST: EquipmentRarities/Delete/5
        // Dzēš ekipējuma retumu
        // Funkcija ER.02
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EquipmentRarities equipmentRarities = db.EquipmentRarities.Find(id);
            db.EquipmentRarities.Remove(equipmentRarities);
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
