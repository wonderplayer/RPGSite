﻿using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using RPGSite.Models;
using System.Web;
using System.IO;
using RPGSite.ViewModels;

namespace RPGSite.Controllers
{
    public class EquipmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Equipments
        public ActionResult Index()
        {
            var equipment = db.Equipment.Include(e => e.Rarity).Include(e => e.Type);
            return View(equipment.ToList());
        }

        // GET: Equipments/Details/5
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
        public ActionResult Create()
        {
            ViewBag.RarityID = new SelectList(db.EquipmentRarities, "ID", "Rarity");
            ViewBag.TypeID = new SelectList(db.EquipmentTypes, "ID", "Type");
            return View();
        }

        // POST: Equipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Description,Price,Picture,TypeID,RarityID")] Equipment equipment, HttpPostedFileBase Picture)
        {
            if (ModelState.IsValid)
            {
                if (Picture != null && Picture.ContentLength > 0)
                {
                    var picture = Path.GetFileName(Picture.FileName);
                    var folder = db.EquipmentTypes.Find(equipment.TypeID).Type.ToString();
                    var databasePath = folder + "/" + picture;
                    equipment.Picture = databasePath;                  
                    var path = Path.Combine(Server.MapPath("~/images/"), databasePath);
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
