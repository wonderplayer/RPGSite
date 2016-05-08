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
    public class PaymentMethodsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PaymentMethods
        public ActionResult Index()
        {
            return View(db.PaymentMethods.ToList());
        }

        // GET: PaymentMethods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentMethods paymentMethods = db.PaymentMethods.Find(id);
            if (paymentMethods == null)
            {
                return HttpNotFound();
            }
            return View(paymentMethods);
        }

        // GET: PaymentMethods/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaymentMethods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Method")] PaymentMethods paymentMethods)
        {
            if (ModelState.IsValid)
            {
                db.PaymentMethods.Add(paymentMethods);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paymentMethods);
        }

        // GET: PaymentMethods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentMethods paymentMethods = db.PaymentMethods.Find(id);
            if (paymentMethods == null)
            {
                return HttpNotFound();
            }
            return View(paymentMethods);
        }

        // POST: PaymentMethods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Method")] PaymentMethods paymentMethods)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paymentMethods).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paymentMethods);
        }

        // GET: PaymentMethods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentMethods paymentMethods = db.PaymentMethods.Find(id);
            if (paymentMethods == null)
            {
                return HttpNotFound();
            }
            return View(paymentMethods);
        }

        // POST: PaymentMethods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PaymentMethods paymentMethods = db.PaymentMethods.Find(id);
            db.PaymentMethods.Remove(paymentMethods);
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
