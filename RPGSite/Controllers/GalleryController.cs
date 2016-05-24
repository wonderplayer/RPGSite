using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RPGSite.Models;
using Microsoft.AspNet.Identity;
using System.IO;

namespace RPGSite.Controllers
{
    public class GalleryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Gallery
        public ActionResult Index()
        {
            var gallery = db.Gallery.Include(g => g.User);
            return View(gallery.ToList());
        }
        [Authorize(Roles = "Admin")]
        // GET: Gallery/Create
        public ActionResult Create()
        {
            return View();
        }
        
        // POST: Gallery/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Picture,Title")] Gallery gallery, HttpPostedFileBase Picture)
        {
            gallery.UserID = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                if (Picture != null && Picture.ContentLength > 0)
                {
                    var picture = Path.GetFileName(Picture.FileName);
                    var folder = "Gallery";
                    var databasePath = folder + "/" + picture;
                    gallery.Picture = databasePath;
                    var path = Path.Combine(Server.MapPath("~/images/"), databasePath);
                    Picture.SaveAs(path);
                }
                db.Gallery.Add(gallery);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gallery);
        }

        [Authorize(Roles = "Admin")]
        // GET: Gallery/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = db.Gallery.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // POST: Gallery/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gallery gallery = db.Gallery.Find(id);
            db.Gallery.Remove(gallery);
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
