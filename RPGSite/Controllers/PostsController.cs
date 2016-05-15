using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RPGSite.Models;
using Microsoft.AspNet.Identity;
using RPGSite.ViewModels;

namespace RPGSite.Controllers
{
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        public ActionResult Index()
        {
            var posts = db.Posts.Where(p => p.IsNews == false).Include(p => p.User);
            return View(posts.ToList());
        }

        // Really bad. Need to do something about validation correctly. And ofc change the comments section
        // GET: Posts/Details/5
        public ActionResult Details(int? id, bool? errors)
        {
            if (errors != null && errors == true)
            {
                ViewBag.Error = "Comment must be less then 255 symbols long";
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Posts posts = db.Posts.Find(id);
            posts.Comments = db.Comments.Where(c => c.PostID == id).ToList();
            PostDetailViewModel post = new PostDetailViewModel
            {
                Comments = posts.Comments,
                Post = posts,
                Date = posts.Updated ?? posts.Created
            };
            if (posts == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Description,IsNews")] Posts posts)
        {
            posts.Created = DateTime.Now;
            posts.UserID = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Posts.Add(posts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(posts);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Posts posts = db.Posts.Find(id);
            if (posts == null)
            {
                return HttpNotFound();
            }
            return View(posts);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Description,Created,UserID")] Posts posts)
        {
            posts.Updated = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Entry(posts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(posts);
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Posts posts = db.Posts.Find(id);
            if (posts == null)
            {
                return HttpNotFound();
            }
            return View(posts);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Posts posts = db.Posts.Find(id);
            db.Posts.Remove(posts);
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

        #region My methods
        [Authorize]
        [HttpPost]
        public ActionResult AddComment(Comments comment, FormCollection values)
        {
            var isError = true;
            comment.Created = DateTime.Now;
            comment.UserID = User.Identity.GetUserId();
            comment.PostID = int.Parse(values["Post.ID"]);
            if (ModelState.IsValid)
            {
                isError = false;
                using (var db = new ApplicationDbContext())
                {
                    db.Comments.Add(comment);
                    db.SaveChanges();
                    
                }
            }
            return RedirectToAction("Details", new { id = comment.PostID, errors = isError });

        }
        #endregion
    }
}
