﻿using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using RPGSite.Models;
using Microsoft.AspNet.Identity;
using RPGSite.ViewModels;
using PagedList;

namespace RPGSite.Controllers
{
    // KLase realizē foruma moduli
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        // Attēlot rakstus FO.05
        public ActionResult Index(int? page)
        {
            var posts = db.Posts.Where(p => p.IsNews == false).Include(p => p.User).OrderBy(p => p.ID);
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(posts.ToPagedList(pageNumber, pageSize));
        }

        // GET: Posts/Details/5
        // Attēlot raksta detaļas
        // Funkcija FO.01
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
        // Attēlot raksta veidošanas skatu
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // Pievienot rakstu datu bāzei
        // Funkcija FO.03
        [Authorize]
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
        // Attēlot raksta rediģēšanas skatu
        // Funkcija FO.06
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
        // Rediģē rakstu
        // Funkcija FO.04
        [Authorize]
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
        // Attēlot raksta dzēšanas skatu
        // Funkcija FO.07
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
        // Dzēst rakstu
        // Funkcija FO.02
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Posts posts = db.Posts.Find(id);
            if (!CanDelete(posts))
            {
                return View("Error");
            }
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

        // Pārbauda, vai lietotājs var dzēst rakstu
        private bool CanDelete(Posts post)
        {
            return User.IsInRole("Admin") || User.Identity.GetUserId() == post.UserID;
        }

        // Pievieno komentāru
        // Funkcija FO.09
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

        // Attēlo rakstu ar komentāriem
        // Funkcija FO.01, FO.10
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

        // Dzēst komentāru
        // Funkcija FO.08
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("DeleteComment")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCommentConfirmed(int id)
        {
            Comments comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = comment.PostID});
        }

        // Attēlot komentāra dzēšanas skatu
        // Funkcija FO.11
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
    }
}
