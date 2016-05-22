using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RPGSite.Models;

namespace RPGSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EquipmentRaritiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EquipmentRarities
        public ActionResult Index()
        {
            return View(db.EquipmentRarities.ToList());
        }

        // GET: EquipmentRarities/Details/5
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: EquipmentRarities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Rarity")] EquipmentRarities equipmentRarities)
        {
            if (ModelState.IsValid)
            {
                db.EquipmentRarities.Add(equipmentRarities);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(equipmentRarities);
        }

        // GET: EquipmentRarities/Edit/5
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Rarity")] EquipmentRarities equipmentRarities)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipmentRarities).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(equipmentRarities);
        }

        // GET: EquipmentRarities/Delete/5
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
