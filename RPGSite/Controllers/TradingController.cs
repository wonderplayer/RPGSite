using Microsoft.AspNet.Identity;
using RPGSite.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace RPGSite.Controllers
{
    [Authorize]
    public class TradingController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        

        // GET: Trading
        public ActionResult Index(string userName)
        {
            var currentUserID = User.Identity.GetUserId();
            var users = db.Users.Where(u => u.Inventories.Count > 0 && u.Id != currentUserID).ToList();
            if (!string.IsNullOrEmpty(userName))
            {
                users = db.Users.Where(
                    u => u.UserName.Contains(userName) &&
                    u.Inventories.Count > 0 &&
                    u.Id != currentUserID).ToList();
            }
            
            return View(users);
        }

        // GET: Trading/Trade
        public ActionResult Trade(string userID)
        {
            if (string.IsNullOrEmpty(userID))
            {
                return RedirectToAction("Index");
            }
            // Inventory with equipment name of current user
            var currentUserID = User.Identity.GetUserId();
            var offererInventory = db.Inventories.Where(i => i.UserID == currentUserID).ToList();
            var allEquipment = db.Equipment.ToList();
            allEquipment.RemoveAll(x => offererInventory.All(y => y.EquipmentID != x.ID));

            // Inventory with equipment name of user who current user wants to trade with
            var wanterInventory = db.Inventories.Where(i => i.UserID == userID).ToList();
            var allEquipment2 = db.Equipment.ToList();
            allEquipment2.RemoveAll(x => wanterInventory.All(y => y.EquipmentID != x.ID));

            ViewBag.OfferedItemID = new SelectList(allEquipment, "ID", "Title");
            ViewBag.WantedItemID = new SelectList(allEquipment2, "ID", "Title");
            ViewBag.WantedUserID = userID;
            ViewBag.User = db.Users.Where(u => u.Id == userID).FirstOrDefault().UserName;
            return View();
        }

        [HttpPost]
        public ActionResult Trade(FormCollection values)
        {
            var offer = new Offers();
            offer.ID = Guid.NewGuid().ToString();
            offer.OfferedItemID = offer.ID;
            offer.WantedItemID = offer.ID;
            offer.OfferStatus = "Pending";

            var offeredItem = new OfferedItem();
            offeredItem.UserID = User.Identity.GetUserId();
            offeredItem.EquipmentID = int.Parse(values["OfferedItemID"]);

            var wantedItem = new WantedItem();
            wantedItem.UserID = values["WantedUserID"];
            wantedItem.EquipmentID = int.Parse(values["WantedItemID"]);
 
            offer.OfferedItem = offeredItem;
            offer.WantedItem = wantedItem;

            db.Offers.Add(offer);
            db.OfferedItem.Add(offeredItem);
            db.WantedItem.Add(wantedItem);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Trades()
        {
            var currentUserID = User.Identity.GetUserId();
            var offers = db.Offers.Where(o => o.WantedItem.UserID == currentUserID).ToList();

            return View(offers);
        }

        public ActionResult Offered()
        {
            var currentUserID = User.Identity.GetUserId();
            var offers = db.Offers.Where(o => o.OfferedItem.UserID == currentUserID).ToList();

            return View(offers);
        }

        public ActionResult Accept(string offererID, string recieverID, int offeredItemID, int recievedItemID, string offerID)
        {
            if (string.IsNullOrEmpty(offerID))
            {
                return HttpNotFound();
            }
            #region Add item to reciever
            Inventories recieverItem = db.Inventories.Where(
                i => i.UserID == recieverID && 
                i.EquipmentID == recievedItemID).FirstOrDefault();
            if (recieverItem == null)
            {
                return HttpNotFound();
            }
            DecreaseQuantity(recieverItem);
            
            Inventories recieverOfferedItem = db.Inventories.Where(
                i => i.UserID == recieverID &&
                i.EquipmentID == offeredItemID)
                .FirstOrDefault();

            if (recieverOfferedItem == null)
            {
                AddItem(recieverOfferedItem ,recieverID, offeredItemID);
            }
            else
            {
                IncreaseQuantity(recieverOfferedItem);
            }
            #endregion

            #region Add item to offerer
            Inventories offererItem = db.Inventories.Where(
                i => i.UserID == offererID && 
                i.EquipmentID == offeredItemID).FirstOrDefault();
            if (offererItem == null)
            {
                return HttpNotFound();
            }
            DecreaseQuantity(offererItem);
            Inventories offererRecievedItem = db.Inventories.Where(
                i => i.UserID == offererID &&
                i.EquipmentID == recievedItemID).FirstOrDefault();

            if (offererRecievedItem == null)
            {
                AddItem(offererRecievedItem, offererID, recievedItemID);
            }
            else
            {
                IncreaseQuantity(offererRecievedItem);
            }
            #endregion

            #region Change offer status
            Offers offer = db.Offers.Where(o => o.ID == offerID).FirstOrDefault();
            if (offer == null)
            {
                return HttpNotFound();
            }

            offer.OfferStatus = "Accepted";
            db.Entry(offer).State = EntityState.Modified;
            #endregion

            db.SaveChanges();
            return View();
        }

        public ActionResult Decline(string offerID)
        {
            if (string.IsNullOrEmpty(offerID))
            {
                return HttpNotFound();
            }
            Offers offer = db.Offers.Where(o => o.ID == offerID).FirstOrDefault();

            if (offer == null)
            {
                return HttpNotFound();
            }
            offer.OfferStatus = "Declined";
            db.Entry(offer).State = EntityState.Modified;

            db.SaveChanges();
            return View();
        }

        private void DecreaseQuantity(Inventories item)
        {
            item.Quantity -= 1;
            if (item.Quantity < 1)
            {
                db.Inventories.Remove(item);
            }
        }

        private void IncreaseQuantity(Inventories item)
        {
            item.Quantity++;
            db.Entry(item).State = EntityState.Modified;
        }

        private void AddItem(Inventories item, string recieverID, int offeredItemID)
        {
            item = new Inventories();
            item.UserID = recieverID;
            item.EquipmentID = offeredItemID;
            item.Quantity = 1;
            db.Inventories.Add(item);
        }
    }
}