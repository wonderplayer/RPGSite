using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using RPGSite.Models;
using Microsoft.AspNet.Identity;
using PagedList;

namespace RPGSite.Controllers
{
    // Klase realizē notikumu moduli
    public class EventsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Events
        // Attēlot notikumus
        // Funkcija NO.05
        public ActionResult Index(int? page)
        {
            var events = db.Events.Include(e => e.User).OrderByDescending(e => e.Created);
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(events.ToPagedList(pageNumber, pageSize));
        }

        // GET: Events/Details/5
        // Attēlot notikumu
        // Funkcija NO.01
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Events events = db.Events.Find(id);
            ViewBag.Date = events.Updated ?? events.Created;
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }

        // GET: Events/Create
        // Attēlot notikuma izveidošanas skatu
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // Pievienot notikumu datu bāzē
        // Funkcija NO.03
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Description,StartDate,EndDate,UserID")] Events events)
        {
            events.Created = DateTime.Now;
            events.UserID = User.Identity.GetUserId();
            if (events.StartDate > events.EndDate)
            {
                ViewBag.DateError = "End date should be greater than the starting date";
                return View(events);
            }
            if (ModelState.IsValid)
            {
                db.Events.Add(events);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(events);
        }

        // GET: Events/Edit/5
        // Attēlot notikuma rediģēšanas skatu
        // Funkcija NO.06
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Events events = db.Events.Find(id);
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // Rediģēt notikumu
        // Funkcija NO.04
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Description,Created,StartDate,EndDate,UserID")] Events events)
        {
            events.Updated = DateTime.Now;
            if (events.StartDate > events.EndDate)
            {
                ViewBag.DateError = "End date should be greater than the starting date";
                return View(events);
            }
            if (ModelState.IsValid)
            {
                db.Entry(events).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(events);
        }

        // GET: Events/Delete/5
        // Atvērt dzēšanas skatu
        // Funkcija NO.07
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Events events = db.Events.Find(id);
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }

        // POST: Events/Delete/5
        // Dzēš notikumu
        // Funkcija NO.02
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Events events = db.Events.Find(id);
            db.Events.Remove(events);
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
