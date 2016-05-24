using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
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

        // GET: Posts/Details/5
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return CreatePostDetailsView(id, null);
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
        [Authorize]
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
            if (CanDelete(posts))
            {
                return View(posts);
            }
            return View("Error");
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
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Posts posts = db.Posts.Find(id);
            ViewBag.Date = posts.Updated ?? posts.Created;
            if (posts == null)
            {
                return HttpNotFound();
            }
            if (CanDelete(posts))
            {
                return View(posts);
            }
            return View("Error");
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

        private bool CanDelete(Posts post)
        {
            return User.IsInRole("Admin") || User.Identity.GetUserId() == post.UserID;
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddComment(PostDetailViewModel postModel, FormCollection values)
        {
            postModel.Comment.Created = DateTime.Now;
            postModel.Comment.UserID = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                using (var db = new ApplicationDbContext())
                {
                    db.Comments.Add(postModel.Comment);
                    db.SaveChanges();
                    
                }
            }

            return CreatePostDetailsView(postModel.Comment.PostID, postModel.Comment);
        }

        private ActionResult CreatePostDetailsView(int? id, Comments comment)
        {
            Posts posts = db.Posts.Find(id);
            posts.Comments = db.Comments.Where(c => c.PostID == id).ToList();
            if (comment == null)
            {
                comment = new Comments
                {
                    PostID = (int)id
                };
            }

            PostDetailViewModel post = new PostDetailViewModel
            {
                Comments = posts.Comments,
                Comment = comment,
                Post = posts,
                Date = posts.Updated ?? posts.Created
            };

            if (posts == null)
            {
                return HttpNotFound();
            }

            return View("Details", post);
        }

        [HttpPost, ActionName("DeleteComment")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCommentConfirmed(int id)
        {
            Comments comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = comment.PostID});
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        #endregion
    }
}
