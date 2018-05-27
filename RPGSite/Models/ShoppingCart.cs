using Microsoft.AspNet.Identity;
using RPGSite.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RPGSite.Models
{
    // Klase palīdz realizēt veikala moduli
    public class ShoppingCart
    {
        ApplicationDbContext db = new ApplicationDbContext();
        string ShoppingCartID { get; set; }
        public const string CartSessionKey = "CartID";

        // Iegūst grozu
        // Funkcija VE.05
        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartID = cart.GetCartId(context);
            return cart;
        }

        // Iegūt grozu no jebkura konrollieta
        // Funkcija VE.05
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        // Pievienot preci grozam
        // Funkcija VE.04
        public void AddToCart(Equipment equipment)
        {
            // Iegūt preci no groza
            var cartItem = db.Carts.SingleOrDefault(c => c.CartID == ShoppingCartID && c.EquipmentID == equipment.ID);

            // Ja prece neeksistē grozā, tad to pievienot, citādāk palielināt daudzumu par 1
            if (cartItem == null)
            {
                cartItem = new Cart
                {
                    EquipmentID = equipment.ID,
                    CartID = ShoppingCartID,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                db.Carts.Add(cartItem);
            }
            else
            {
                cartItem.Count++;
            }
            db.SaveChanges();
        }

        // Izņemt no groza
        // Funkcija VE.06
        public int RemoveFromCart(int id)
        {
            // Iegūt grozu
            var cartItem = db.Carts.Single(
                cart => cart.CartID == ShoppingCartID && cart.RecordID == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                // Ja daudzums lielāks par 1, tad samazināt daudzumu, citādāk izņemt preci no groza
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    db.Carts.Remove(cartItem);
                }
                //Save changes
                db.SaveChanges();
            }
            return itemCount;
        }

        // Izņemt visas preces no groza pēc pirkšanas
        public void EmptyCart()
        {
            var cartItems = db.Carts.Where(cart => cart.CartID == ShoppingCartID);

            foreach (var cartItem in cartItems)
            {
                db.Carts.Remove(cartItem);
            }
            db.SaveChanges();
        }

        // Iegūit preces grozā
        public List<Cart> GetCartItems()
        {
            return db.Carts.Where(cart => cart.CartID == ShoppingCartID).ToList();
        }

        // Iegūt groza preču dauduzmu
        public int GetCount()
        {
            int? count = (from cartItems in db.Carts
                          where cartItems.CartID == ShoppingCartID
                          select (int?)cartItems.Count).Sum();
            return count ?? 0;
        }

        // Iegūt groza summu
        public decimal GetTotal()
        {
            decimal? total = (from cartItems in db.Carts
                              where cartItems.CartID == ShoppingCartID
                              select (int?)cartItems.Count * cartItems.Equipment.Price).Sum();
            return total ?? decimal.Zero;
        }

        // Iegūt pirkšana
        // Funkcija VE.07
        public int CreateOrder(Orders order)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems();

            // Iziet caur visām groza precēm
            foreach (var item in cartItems)
            {
                // Izveidot jaunun ierakstu pirkuma priekšmetiem
                var orderItem = new OrderItems
                {
                    EquipmentID = item.EquipmentID,
                    OrderID = order.ID,
                    Quantity = item.Count,
                    Total = item.Count * item.Equipment.Price
                };

                var inventoryItem = db.Inventories.Where(i => i.UserID == order.UserID && i.EquipmentID == item.EquipmentID).FirstOrDefault();

                // Ja prece jau ir pirkuma precēs, tad palielināt tās daudzumu, citādāk pievienot
                if (inventoryItem != null)
                {
                    inventoryItem.Quantity += item.Count;
                    db.Entry(inventoryItem).State = EntityState.Modified;
                }
                else
                {
                    inventoryItem = new Inventories
                    {
                        EquipmentID = item.EquipmentID,
                        Quantity = item.Count,
                        UserID = order.UserID
                    };
                    db.Inventories.Add(inventoryItem);
                }

                orderTotal += (item.Count * item.Equipment.Price);

                db.OrderItems.Add(orderItem);
            }
            order.Total = orderTotal;

            db.SaveChanges();
            EmptyCart();
            return order.ID;
        }

        // Iegūst groza ID no pārlūka sīkfailiem
        public string GetCartId(HttpContextBase context)
        {
            // Pārbaudīt, vai sīkfailos jau ir iepirkumu grozs
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    // Izveidot iepirkuma grozu, ja lietotājs ir autentificēts
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    // Uzģenerēt ieprikuma groza ID, ja lietotājs nav autentificēts
                    Guid tempCartId = Guid.NewGuid();
                    // Saglabāt to ID kā sīkfalu
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }
        
        // Maina groza ID no GUID uz autentificētā lietotāja e-pastu
        public void MigrateCart(string email)
        {
            var shoppingCart = db.Carts.Where(
                c => c.CartID == ShoppingCartID);

            foreach (Cart item in shoppingCart)
            {
                item.CartID = email;
            }
            db.SaveChanges();
        }
    }
}