using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RPGSite.Models;
using Microsoft.AspNet.Identity;
using System.IO;
using PagedList;
using System;
using System.Diagnostics;

namespace RPGSite.Controllers
{
    // Klase realizē galerijas moduli
    public class GalleryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Gallery
        // Attēlot galeriju
        // Funkcija GA.01
        public ActionResult Index(int? page)
        {
            try
            {
                var gallery = db.Gallery.Include(g => g.User).OrderBy(g => g.ID);
                int pageSize = 6;
                int pageNumber = (page ?? 1);
                return View(gallery.ToPagedList(pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                Trace.Write(ex);
                return null;
            }
            
        }
        [Authorize(Roles = "Admin")]
        // GET: Gallery/Create
        // Attēlot bildes pievienošanas skatu
        public ActionResult Create()
        {
            return View();
        }
        
        // POST: Gallery/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // Pievienot bildi datu bāzē
        // Funkcija GA.03
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Picture,Title")] Gallery gallery, HttpPostedFileBase Picture)
        {
            gallery.UserID = User.Identity.GetUserId();
            if (Picture != null && Picture.ContentLength > 0)
            {
                if (ModelState.IsValid)
                {
                    // Pārbaudīt, vai bilde atbilst visiem nosacījumiem
                    // Tad saglabāt bildi datu bāzē
                    var picture = Path.GetFileName(Picture.FileName);
                    var extenstion = Path.GetExtension(Picture.FileName);
                    if (extenstion != ".jpg" && extenstion != ".png" && extenstion != ".jpeg" && extenstion != ".gif")
                    {
                        ViewBag.PictureError = "The picture should be format of .jpg, .gif, .png or .jpeg";
                        return View(gallery);
                    }
                    var folder = "Gallery";
                    var databasePath = folder + "/" + picture;
                    gallery.Picture = databasePath;
                    var path = Path.Combine(Server.MapPath("~/images/"), databasePath);
                    Picture.SaveAs(path);
                    db.Gallery.Add(gallery);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Error = "Picture is not selected.";
            return View(gallery);
        }

        // GET: Gallery/Delete/5
        // Attēlot bildes dzēšanas skatu
        // Funkcija GA.04
        [Authorize(Roles = "Admin")]
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
        // Dzēst bildi
        // Funkcija GA.02
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
