using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using RPGSite.Models;

namespace RPGSite.Controllers
{
    // Klase realizē apmaksas veidu moduli
    [Authorize(Roles = "Admin")]
    public class PaymentMethodsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PaymentMethods
        // Attēlot apmaksas veidus
        // Funkcija AP.05
        public ActionResult Index()
        {
            return View(db.PaymentMethods.ToList());
        }

        // GET: PaymentMethods/Details/5
        // Attēlot apmaksas veidu
        // Funkcija AP.01
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
        // Attēlot apmaksas veida izveidošanas skatu
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaymentMethods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // Pievienot apmaksas veidu datu bāzē
        // Funkcija AP.03
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
        // Attēlot apmaksas veida rediģēšanas skatu
        // Funkcija AP.06
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
        // Rediģēt apmaksas veidu
        // Funkcija AP.04
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
        // Atvērt dzēšanas skatu
        // Funkcija AP.07
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
        // Dzēš apmaksas veidu
        // Funkcija AP.02
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
