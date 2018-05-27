using Microsoft.AspNet.Identity;
using RPGSite.Models;
using System;
using System.Web.Mvc;

namespace RPGSite.Controllers
{
    // Foruma moduli
    // Klase apraksta funkciju FO.09
    public class CommentsController : Controller
    {
        // GET: Comments
        // Attēlot komentāra pievienošanas skatu
        public ActionResult Create(int PostID)
        {
            var newComment = new Comments();
            newComment.PostID = PostID;

            return PartialView(newComment);
        }
        
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Pievienot komentāru
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