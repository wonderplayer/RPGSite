namespace RPGSite.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        // Klase ir priekš datu ievietošanas datu bāzē pie tās izveidošanas
        protected override void Seed(ApplicationDbContext context)
        {
            var passwordHash = new PasswordHasher();
            string password = passwordHash.HashPassword("Test!!2");
            context.Roles.AddOrUpdate(
                r => r.Name,
                new IdentityRole { Name = "Admin" },
                new IdentityRole { Name = "User" }
                );
            SaveChanges(context);

            ApplicationUser admin = new ApplicationUser
            {
                UserName = "Admin",
                Email = "admin@admin.com",
                PasswordHash = password,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            context.Users.AddOrUpdate(
                u => u.UserName,
                admin,
                new ApplicationUser { UserName = "Roland", Email = "roland@roland.com", PasswordHash = password, SecurityStamp = Guid.NewGuid().ToString() },
                new ApplicationUser { UserName = "Edgar", Email = "edgar@edgar.com", PasswordHash = password, SecurityStamp = Guid.NewGuid().ToString() }
                );
            SaveChanges(context);

            var userIDs = getUserIDs(context);

            AddAdminToRole(context, admin.Id);

            context.EquipmentRarities.AddOrUpdate(
                er => er.ID,
                new EquipmentRarities { ID = 1, Rarity = "Ordinary" },
                new EquipmentRarities { ID = 2, Rarity = "Rare" },
                new EquipmentRarities { ID = 3, Rarity = "Very rare" },
                new EquipmentRarities { ID = 4, Rarity = "Epic" },
                new EquipmentRarities { ID = 5, Rarity = "Mithril" },
                new EquipmentRarities { ID = 6, Rarity = "Titanium" },
                new EquipmentRarities { ID = 7, Rarity = "Legendary" }
                );
            SaveChanges(context);

            var rarityIDs = getEquipmentRarityIDs(context);

            context.EquipmentTypes.AddOrUpdate(
                et => et.ID,
                new EquipmentTypes { ID = 1, Type = "Sword" },
                new EquipmentTypes { ID = 2, Type = "Shield" },
                new EquipmentTypes { ID = 3, Type = "Gloves" },
                new EquipmentTypes { ID = 4, Type = "Armor" },
                new EquipmentTypes { ID = 5, Type = "Pants" },
                new EquipmentTypes { ID = 6, Type = "Helmet" },
                new EquipmentTypes { ID = 7, Type = "Boots" }
                );
            SaveChanges(context);

            var typeIDs = getEquipmentTypeIDs(context);

            context.Equipment.AddOrUpdate(
                e => e.Title,
                new Equipment { ID = 1, Title = "Boots of swiftness", Description = "With these boots no one will run away from you anymore!", Price = 1.99m, TypeID = typeIDs[6], RarityID = rarityIDs[1], Picture = "\\Boots\\e1.png" },
                new Equipment { ID = 2, Title = "Demons helmet", Description = "This helmet was taken off from the very demon!", Price = 2.99m, TypeID = typeIDs[5], RarityID = rarityIDs[3], Picture = "\\Helmet\\e2.jpg" },
                new Equipment { ID = 3, Title = "Shield of Achellies", Description = "The mighty Achellies dropped his shield in a war... and we picked it up ;)", Price = 10.99m, TypeID = typeIDs[1], RarityID = rarityIDs[3], Picture = "\\Shield\\e3.jpg" },
                new Equipment { ID = 4, Title = "Warriors armor", Description = "This armor is the best if you are going to fight some demons. It`s style and defens are extraordinary!", Price = 15.99m, TypeID = typeIDs[3], RarityID = rarityIDs[4], Picture = "\\Armor\\e4.jpg" },
                new Equipment { ID = 5, Title = "Shield", Description = "Just some basic shield for all n00bs out there.", Price = 0.99m, TypeID = typeIDs[1], RarityID = rarityIDs[0], Picture = "\\Shield\\e5.png" },
                new Equipment { ID = 6, Title = "Armor of Gods", Description = "Ohh... Do you know better armor than the armor of Gods? I don`t.", Price = 20.99m, TypeID = typeIDs[3], RarityID = rarityIDs[5], Picture = "\\Armor\\e6.jpg" },
                new Equipment { ID = 4, Title = "Armor of Legends", Description = "The armor of Legends. What can I say? Just buy it ok?", Price = 30.99m, TypeID = typeIDs[3], RarityID = rarityIDs[6], Picture = "\\Armor\\e7.png" }
                );
            SaveChanges(context);

            var equipmentIDs = getEquipmentIDs(context);

            context.Events.AddOrUpdate(
                e => e.ID,
                new Events { ID = 1, Title = "Slay the draon", Description = "Anyone who slay sthe dragon will be granted with magical sword of destruction!", Created = new DateTime(2016, 4, 25), Updated = new DateTime(2016, 4, 25), StartDate = new DateTime(2016, 5, 25), EndDate = new DateTime(2016, 5, 28), UserID = userIDs[0] },
                new Events { ID = 2, Title = "Catch the rabbit", Description = "Anyone who catches the fastest rabbit on this planet will be granted with magical shield of protection!", Created = new DateTime(2016, 4, 25), Updated = new DateTime(2016, 4, 25), StartDate = new DateTime(2016, 5, 29), EndDate = new DateTime(2016, 5, 30), UserID = userIDs[0]}
                );
            SaveChanges(context);

            context.Posts.AddOrUpdate(
                p => p.ID,
                new Posts { ID = 1, Title = "New update 5.1.0 is comming!", Description = "Our new update 5.1.0. is comming live on all servers on 25th of June.", Created = new DateTime(2016, 4, 26), Updated = new DateTime(2016, 4, 27), IsNews = true, UserID = userIDs[0] },
                new Posts { ID = 2, Title = "New equipment!", Description = "New equipments will be here soon. You will be aviable to buy it starting from next week on Thursday 25th of June.", Created = new DateTime(2016, 4, 24), Updated = new DateTime(2016, 4, 28), IsNews = true, UserID = userIDs[0] },
                new Posts { ID = 3, Title = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.", Description = "Praesent eget pellentesque odio. Integer laoreet, augue in imperdiet faucibus, purus metus ornare magna, nec mattis sem lorem quis nibh. Aenean fermentum est leo, at semper dui pulvinar ut. Phasellus id neque ut eros varius placerat non in arcu. In tincidunt magna ante, in feugiat urna rhoncus vel. Aliquam non felis eu libero rutrum mattis quis semper massa. Proin et dolor tortor. Sed eleifend erat id risus cursus, a porta eros rhoncus.", Created = new DateTime(2016, 4, 30), Updated = new DateTime(2016, 5, 1), IsNews = true, UserID = userIDs[0] },
                new Posts { ID = 4, Title = "Aliquam erat volutpat!", Description = "Aliquam erat volutpat. Duis auctor imperdiet elit, et consectetur nulla aliquet vitae. Proin faucibus dolor eget dictum rhoncus. Suspendisse mattis gravida eleifend. Fusce rutrum egestas dolor, quis gravida eros dignissim eu. Etiam tincidunt ante efficitur, semper arcu et, aliquet purus. Etiam sagittis urna sit amet cursus venenatis.", Created = new DateTime(2016, 4, 30), Updated = new DateTime(2016, 4, 30), IsNews = false, UserID = userIDs[1] },
                new Posts { ID = 5, Title = "Praesent eget?", Description = "Praesent eget pellentesque odio. Integer laoreet, augue in imperdiet faucibus, purus metus ornare magna, nec mattis sem lorem quis nibh.", Created = new DateTime(2016, 4, 10), Updated = new DateTime(2016, 5, 10), IsNews = false, UserID = userIDs[1] }
                );
            SaveChanges(context);

            var postIDs = getPostIDs(context);

            context.Comments.AddOrUpdate(
                c => c.ID,
                new Comments { ID = 1, Comment = "Aliquam erat volutpat!", Created = new DateTime(2016, 4, 26), PostID = postIDs[3], UserID = userIDs[0] },
                new Comments { ID = 2, Comment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.", Created = new DateTime(2016, 4, 28), PostID = postIDs[3], UserID = userIDs[1] },
                new Comments { ID = 2, Comment = "Sed dapibus eu turpis eget auctor. Nullam dolor neque, lacinia eu ligula vel, aliquet tempor sapien. Nullam euismod vel purus vel pretium. Quisque finibus non lectus facilisis feugiat.", Created = new DateTime(2016, 4, 28), PostID = postIDs[3], UserID = userIDs[1] }
                );
            SaveChanges(context);

            context.PaymentMethods.AddOrUpdate(
                pm => pm.ID,
                new PaymentMethods { ID = 1, Method = "Paypal" },
                new PaymentMethods { ID = 2, Method = "Visa" },
                new PaymentMethods { ID = 3, Method = "Master Card" }
                );
            SaveChanges(context);

            var paymentMethodIDs = getPaymentMethodIDs(context);

            context.Inventories.AddOrUpdate(
                i => i.ID,
                new Inventories { ID = 1, EquipmentID = equipmentIDs[0], Quantity = 5, UserID = userIDs[0] },
                new Inventories { ID = 2, EquipmentID = equipmentIDs[0], Quantity = 1, UserID = userIDs[1] },
                new Inventories { ID = 3, EquipmentID = equipmentIDs[1], Quantity = 2, UserID = userIDs[0] },
                new Inventories { ID = 4, EquipmentID = equipmentIDs[4], Quantity = 3, UserID = userIDs[1] }
                );
            SaveChanges(context);

            context.Orders.AddOrUpdate(
                o => o.ID,
                new Orders { ID = 1, OrderDate = new DateTime(2016, 4, 28), PaymentMethodID = paymentMethodIDs[0], UserID = userIDs[0], Total = 9.95m },
                new Orders { ID = 2, OrderDate = new DateTime(2016, 4, 29), PaymentMethodID = paymentMethodIDs[1], UserID = userIDs[0], Total = 7.97m },
                new Orders { ID = 3, OrderDate = new DateTime(2016, 4, 30), PaymentMethodID = paymentMethodIDs[0], UserID = userIDs[1], Total = 2.97m }
                );
            SaveChanges(context);

            var orderIDs = getOrderIDs(context);

            context.OrderItems.AddOrUpdate(
                oi => oi.ID,
                new OrderItems { ID = 1, EquipmentID = equipmentIDs[0], OrderID = orderIDs[0], Quantity = 5, Total = 9.95m },
                new OrderItems { ID = 2, EquipmentID = equipmentIDs[0], OrderID = orderIDs[1], Quantity = 1, Total = 1.99m },
                new OrderItems { ID = 3, EquipmentID = equipmentIDs[1], OrderID = orderIDs[1], Quantity = 2, Total = 5.98m },
                new OrderItems { ID = 4, EquipmentID = equipmentIDs[4], OrderID = orderIDs[2], Quantity = 3, Total = 2.97m }
                );

            SaveChanges(context);

            context.Gallery.AddOrUpdate(
                g => g.ID,
                new Gallery { Title = "Screenshot from game", UserID = userIDs[0], Picture = "\\Gallery\\2.jpg" },
                new Gallery { Title = "Beginner level", UserID = userIDs[0], Picture = "\\Gallery\\3.jpg" },
                new Gallery { Title = "Blader", UserID = userIDs[0], Picture = "\\Gallery\\4.png" },
                new Gallery { Title = "All characters", UserID = userIDs[0], Picture = "\\Gallery\\5.jpg" },
                new Gallery { Title = "Divine battle", UserID = userIDs[0], Picture = "\\Gallery\\6.jpg" },
                new Gallery { Title = "Assassin", UserID = userIDs[0], Picture = "\\Gallery\\7.jpg" },
                new Gallery { Title = "Shielder", UserID = userIDs[0], Picture = "\\Gallery\\8.jpg" }
                );
        }

        private void AddAdminToRole(ApplicationDbContext context, string Id)
        {
            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);
            manager.AddToRole(Id, "Admin");
        }

        private void SaveChanges(DbContext context)
        {
            try {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex) {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors) {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors) {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                ); 
            }
        }

        #region Id getters
        private int[] getOrderIDs(ApplicationDbContext context)
        {
            List<Orders> orders = new List<Orders>();
            orders = context.Orders.ToList();
            var orderIDs = new int[orders.Count];
            for (int i = 0; i < orders.Count; i++)
            {
                orderIDs[i] = orders[i].ID;
            }
            return orderIDs;
        }


        private int[] getEquipmentIDs(ApplicationDbContext context)
        {
            List<Equipment> equipment = new List<Equipment>();
            equipment = context.Equipment.ToList();
            var equipmentIDs = new int[equipment.Count];
            for (int i = 0; i < equipment.Count; i++)
            {
                equipmentIDs[i] = equipment[i].ID;
            }
            return equipmentIDs;

        }

        private string[] getUserIDs(ApplicationDbContext context)
        {
            List<ApplicationUser> users = new List<ApplicationUser>();
            users = context.Users.ToList();
            var userIDs = new string[users.Count];
            for (int i = 0; i < users.Count; i++)
            {
                userIDs[i] = users[i].Id;
            }
            return userIDs;
        }

        private int[] getEquipmentTypeIDs(ApplicationDbContext context)
        {
            List<EquipmentTypes> types = new List<EquipmentTypes>();
            types = context.EquipmentTypes.ToList();
            var typeIDs = new int[types.Count];
            for (int i = 0; i < types.Count; i++)
            {
                typeIDs[i] = types[i].ID;
            }
            return typeIDs;
        }

        private int[] getEquipmentRarityIDs(ApplicationDbContext context)
        {
            List<EquipmentRarities> rarities = new List<EquipmentRarities>();
            rarities = context.EquipmentRarities.ToList();
            var rarityIDs = new int[rarities.Count];
            for (int i = 0; i < rarities.Count; i++)
            {
                rarityIDs[i] = rarities[i].ID;
            }
            return rarityIDs;
        }

        private int[] getPostIDs(ApplicationDbContext context)
        {
            List<Posts> posts = new List<Posts>();
            posts = context.Posts.ToList();
            var postIDs = new int[posts.Count];
            for (int i = 0; i < posts.Count; i++)
            {
                postIDs[i] = posts[i].ID;
            }
            return postIDs;
        }

        private int[] getPaymentMethodIDs(ApplicationDbContext context)
        {
            List<PaymentMethods> paymentMethods = new List<PaymentMethods>();
            paymentMethods = context.PaymentMethods.ToList();
            var pmIDs = new int[paymentMethods.Count];
            for (int i = 0; i < paymentMethods.Count; i++)
            {
                pmIDs[i] = paymentMethods[i].ID;
            }
            return pmIDs;
        }
        #endregion

    }
}
