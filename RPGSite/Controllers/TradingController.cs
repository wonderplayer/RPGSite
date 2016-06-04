using RPGSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RPGSite.Controllers
{
    public class TradingController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        

        // GET: Trading
        public ActionResult Index(string userName)
        {
            var users = db.Users.ToList();
            if (!string.IsNullOrEmpty(userName))
            {
                users = db.Users.Where(u => u.UserName.Contains(userName)).ToList();
            }
            
            return View(users);
        }

        
    }
}