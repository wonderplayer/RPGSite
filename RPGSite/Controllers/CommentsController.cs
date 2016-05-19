using Microsoft.AspNet.Identity;
using RPGSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RPGSite.Controllers
{
    public class CommentsController : Controller
    {
        // GET: Comments
        public ActionResult Create(int PostID)
        {
            var newComment = new Comments();
            newComment.PostID = PostID;

            return PartialView(newComment);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Comments comment)
        {
            comment.Created = DateTime.Now;
            comment.UserID = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                using (var db = new ApplicationDbContext())
                {
                    db.Comments.Add(comment);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Details", "Posts", new { id = comment.PostID });
        }
    }
}