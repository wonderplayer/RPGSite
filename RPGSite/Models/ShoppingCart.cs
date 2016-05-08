using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RPGSite.Models
{
    public class ShoppingCart
    {
        ApplicationDbContext db = new ApplicationDbContext();
        string ShoppingCartID { get; set; }
        public const string CartSessionKey = "CartID";
        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartID = cart.GetCartId(context);
            return cart;
        }
        //Helper method to simplify shopping cart calls
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }
        public void AddToCart(Equipment equipment)
        {
            //Get the matching cart and equipment instances
            var cartItem = db.Carts.SingleOrDefault(c => c.CartID == ShoppingCartID && c.EquipmentID == equipment.ID);

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
                //If the item does exist in the cart, then add one to quantity
                cartItem.Count++;
            }
            //Save changes
            db.SaveChanges();
        }
        public int RemoveFromCart(int id)
        {
            //Get the cart
            var cartItem = db.Carts.Single(
                cart => cart.CartID == ShoppingCartID && cart.RecordID == id);

            int itemCount = 0;

            if (cartItem != null)
            {
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
        public void EmptyCart()
        {
            var cartItems = db.Carts.Where(cart => cart.CartID == ShoppingCartID);

            foreach (var cartItem in cartItems)
            {
                db.Carts.Remove(cartItem);
            }
            //Save changes
            db.SaveChanges();
        }
        public List<Cart> GetCartItems()
        {
            return db.Carts.Where(cart => cart.CartID == ShoppingCartID).ToList();
        }
        public int GetCount()
        {
            //Get the count of each item in the cart and sum them up
            int? count = (from cartItems in db.Carts
                          where cartItems.CartID == ShoppingCartID
                          select (int?)cartItems.Count).Sum();
            //Return 0 if all entries are null
            return count ?? 0;
        }
        public decimal GetTotal()
        {
            //Multiply equipment price by count of that equipment to get
            //the current price for each of those albums in the cart
            //sum all album price totals to get the cart total
            decimal? total = (from cartItems in db.Carts
                              where cartItems.CartID == ShoppingCartID
                              select (int?)cartItems.Count * cartItems.Equipment.Price).Sum();
            return total ?? decimal.Zero;
        }
        public int CreateOrder(Orders order)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems();
            //Iterate over the items in the cart,
            //adding th order details for each
            foreach (var item in cartItems)
            {
                var orderItem = new OrderItems
                {
                    EquipmentID = item.EquipmentID,
                    OrderID = order.ID,
                    Quantity = item.Count,
                    Total = item.Count * item.Equipment.Price    //Maybe need to change to unit price not total price
                };
                //Set the order total of the shopping cart
                orderTotal += (item.Count * item.Equipment.Price);

                db.OrderItems.Add(orderItem);
            }
            //Set the order's total to the orderTotal count
            order.Total = orderTotal;

            //Save the order
            db.SaveChanges();
            //Empty the shopping cart
            EmptyCart();
            //Return the OrderID as the confirmation number
            return order.ID;
        }
        //We're using HttpContextBase to allow acces to cookies
        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    //Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    //Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }
        //When a user ha logged in, migrate their shopping cart to
        //ve associated with theid username
        public void MigrateCart(string userName)
        {
            var shoppingCart = db.Carts.Where(
                c => c.CartID == ShoppingCartID);

            foreach (Cart item in shoppingCart)
            {
                item.CartID = userName;
            }
            db.SaveChanges();
        }
    }
}