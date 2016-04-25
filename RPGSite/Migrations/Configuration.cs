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
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Text;
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var passwordHash = new PasswordHasher();
            string password = passwordHash.HashPassword("Test112");
            context.Roles.AddOrUpdate(
                r => r.Name,
                new IdentityRole { Name = "Admin" },
                new IdentityRole { Name = "User" }
                );
            SaveChanges(context);

            context.Users.AddOrUpdate(
                u => u.UserName,
                new ApplicationUser { UserName = "Admin", Email = "admin@admin.com", PasswordHash = password, },
                new ApplicationUser { UserName = "Roland", Email = "roland@roland.com", PasswordHash = password },
                new ApplicationUser { UserName = "Edgar", Email = "edgar@edgar.com", PasswordHash = password }
                );
            SaveChanges(context);

            string user1ID = userIDFinder(1, context);
            string user2ID = userIDFinder(1, context);

            context.EquipmentRarities.AddOrUpdate(
                er => er.ID,
                new EquipmentRarities { ID = 1, Rarity = "Rare" },
                new EquipmentRarities { ID = 2, Rarity = "Epic" }
                );
            SaveChanges(context);

            var rarity1 = equipmentRarityIDFinder(1, context);
            var rarity2 = equipmentRarityIDFinder(2, context);

            context.EquipmentTypes.AddOrUpdate(
                et => et.ID,
                new EquipmentTypes { ID = 1, Type = "Sword" },
                new EquipmentTypes { ID = 2, Type = "Shield" }
                );
            SaveChanges(context);

            var type1 = equipmentTypeIDFinder(1, context);
            var type2 = equipmentTypeIDFinder(2, context);

            Image sword1 = Image.FromFile("D:\\VisualStudio2015\\Projects\\RPGSite\\RPGSite\\Images\\Swords\\Sword.jpg");
            Image sword2 = Image.FromFile("D:\\VisualStudio2015\\Projects\\RPGSite\\RPGSite\\Images\\Swords\\GuardianAngelSword.gif");
            Image shield1 = Image.FromFile("D:\\VisualStudio2015\\Projects\\RPGSite\\RPGSite\\Images\\Shields\\Shield.jpg");
            var convertedSword1 = jpegImageToByteArray(sword1);
            var convertedSword2 = jpegImageToByteArray(sword2);
            var convertedShield1 = jpegImageToByteArray(shield1);

            context.Equipment.AddOrUpdate(
                e => e.ID,
                new Equipment { ID = 1, Title = "Dragon sword", Description = "Sword form dragons skin", Price = 10.99, TypeID = type1, RarityID = rarity1, Picture = convertedSword1},
                new Equipment { ID = 2, Title = "Dragons shield", Description = "Shield form dragons skin", Price = 8.99, TypeID = type2, RarityID = rarity1, Picture = convertedShield1},
                new Equipment { ID = 3, Title = "Warriors magic sword", Description = "Sword of mighty magic warrior", Price = 20.99, TypeID = type1, RarityID = rarity2, Picture = convertedSword2 }
                );
            SaveChanges(context);

            var equipment1 = equipmentIDFinder(1, context);
            var equipment2 = equipmentIDFinder(2, context);
            var equipment3 = equipmentIDFinder(3, context);

            context.Events.AddOrUpdate(
                e => e.ID,
                new Events { ID = 1, Title = "Slay the draon", Description = "Anyone who slay sthe dragon will be granted with magical sword of destruction!", Created = new DateTime(2016, 4, 25), Updated = new DateTime(2016, 4, 25), StartDate = new DateTime(2016, 5, 25), EndDate = new DateTime(2016, 5, 28), UserID = user1ID },
                new Events { ID = 2, Title = "Catch the rabbit", Description = "Anyone who catches the fastest rabbit on this planet will be granted with magical shield of protection!", Created = new DateTime(2016, 4, 25), Updated = new DateTime(2016, 4, 25), StartDate = new DateTime(2016, 5, 29), EndDate = new DateTime(2016, 5, 30), UserID = user1ID }
                );
            SaveChanges(context);

            context.Posts.AddOrUpdate(
                p => p.ID,
                new Posts { ID = 1, Title = "New update 5.1.0 is comming!", Description = "Our new update 5.1.0. is comming live on all servers on 25th of June.", Created = new DateTime(2016, 4, 26), Updated = new DateTime(2016, 4, 27), IsNews = true, UserID = user1ID },
                new Posts { ID = 2, Title = "New equipment!", Description = "New equipments will be here soon. You will be aviable to buy it starting from next week on Thursday 25th of June.", Created = new DateTime(2016, 4, 24), Updated = new DateTime(2016, 4, 28), IsNews = true, UserID = user1ID },
                new Posts { ID = 3, Title = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.", Description = "Praesent eget pellentesque odio. Integer laoreet, augue in imperdiet faucibus, purus metus ornare magna, nec mattis sem lorem quis nibh. Aenean fermentum est leo, at semper dui pulvinar ut. Phasellus id neque ut eros varius placerat non in arcu. In tincidunt magna ante, in feugiat urna rhoncus vel. Aliquam non felis eu libero rutrum mattis quis semper massa. Proin et dolor tortor. Sed eleifend erat id risus cursus, a porta eros rhoncus.", Created = new DateTime(2016, 4, 30), Updated = new DateTime(2016, 5, 1), IsNews = true, UserID = user1ID },
                new Posts { ID = 4, Title = "Aliquam erat volutpat!", Description = "Aliquam erat volutpat. Duis auctor imperdiet elit, et consectetur nulla aliquet vitae. Proin faucibus dolor eget dictum rhoncus. Suspendisse mattis gravida eleifend. Fusce rutrum egestas dolor, quis gravida eros dignissim eu. Etiam tincidunt ante efficitur, semper arcu et, aliquet purus. Etiam sagittis urna sit amet cursus venenatis.", Created = new DateTime(2016, 4, 30), Updated = new DateTime(2016, 4, 30), IsNews = false, UserID = user2ID },
                new Posts { ID = 5, Title = "Praesent eget?", Description = "Praesent eget pellentesque odio. Integer laoreet, augue in imperdiet faucibus, purus metus ornare magna, nec mattis sem lorem quis nibh.", Created = new DateTime(2016, 4, 10), Updated = new DateTime(2016, 5, 10), IsNews = false, UserID = user2ID }
                );
            SaveChanges(context);

            var post4 = postIDFinder(4, context);
            var post5 = postIDFinder(5, context);

            context.Comments.AddOrUpdate(
                c => c.ID,
                new Comments { ID = 1, Comment = "Aliquam erat volutpat!", Created = new DateTime(2016, 4, 26), PostID = post4, UserID = user1ID },
                new Comments { ID = 2, Comment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.", Created = new DateTime(2016, 4, 28), PostID = post4, UserID = user2ID }
                );
            SaveChanges(context);

            context.PaymentMethods.AddOrUpdate(
                pm => pm.ID,
                new PaymentMethods { ID = 1, Method = "Paypal" },
                new PaymentMethods { ID = 2, Method = "Visa" },
                new PaymentMethods { ID = 3, Method = "Master Card" }
                );
            SaveChanges(context);

            var pm1 = paymentMethodIDFinder(1, context);
            var pm2 = paymentMethodIDFinder(2, context);
            var pm3 = paymentMethodIDFinder(3, context);

            context.SentMessages.AddOrUpdate(
                sm => sm.ID,
                new SentMessages { ID = 1, DateSent = new DateTime(2016, 5, 26), Message = "Hello there.", Title = "How 'ya feelin?", Read = false, UserID = user1ID },
                new SentMessages { ID = 2, DateSent = new DateTime(2016, 5, 28), Message = "For Freddy?", Title = "Are you ready?", Read = false, UserID = user1ID }
                );
            SaveChanges(context);

            context.RecievedMessages.AddOrUpdate(
                rm => rm.ID,
                new RecievedMessages { ID = 1, UserID = user2ID, SentMessageID = 1 },
                new RecievedMessages { ID = 2, UserID = user2ID, SentMessageID = 2 }
                );
            SaveChanges(context);

            context.Trades.AddOrUpdate(
                t => t.ID,
                new Trades { ID = 1, TradeDate = new DateTime(2016, 5, 23), Status = "declined" },
                new Trades { ID = 2, TradeDate = new DateTime(2016, 5, 26), Status = "new" },
                new Trades { ID = 3, TradeDate = new DateTime(2016, 5, 27), Status = "accepted" }
                );
            SaveChanges(context);

            context.WantedItems.AddOrUpdate(
                wi => wi.ID,
                new WantedItems { ID = 1, ItemID = 1, TradeID = 1, UserID = user1ID },
                new WantedItems { ID = 2, ItemID = 1, TradeID = 2, UserID = user1ID },
                new WantedItems { ID = 3, ItemID = 2, TradeID = 3, UserID = user1ID }
                );
            SaveChanges(context);

            context.OfferedItems.AddOrUpdate(
                oi => oi.ID,
                new OfferedItems { ID = 1, ItemID = 2, TradeID = 1, UserID = user2ID },
                new OfferedItems { ID = 2, ItemID = 2, TradeID = 2, UserID = user2ID },
                new OfferedItems { ID = 3, ItemID = 1, TradeID = 3, UserID = user2ID }
                );
            SaveChanges(context);

            context.Inventories.AddOrUpdate(
                i => i.ID,
                new Inventories { ID = 1, EquipmentID = 1, Quantity = 2, UserID = user1ID },
                new Inventories { ID = 2, EquipmentID = 1, Quantity = 4, UserID = user2ID },
                new Inventories { ID = 3, EquipmentID = 2, Quantity = 1, UserID = user1ID },
                new Inventories { ID = 4, EquipmentID = 2, Quantity = 7, UserID = user2ID }
                );
            SaveChanges(context);

            context.Orders.AddOrUpdate(
                o => o.ID,
                new Orders { ID = 1, OrderDate = new DateTime(2016, 4, 28), PaymentMethodID = 1, UserID = user1ID, Total = 54.95 },
                new Orders { ID = 2, OrderDate = new DateTime(2016, 4, 29), PaymentMethodID = 2, UserID = user1ID, Total = 28.97 },
                new Orders { ID = 3, OrderDate = new DateTime(2016, 4, 30), PaymentMethodID = 1, UserID = user2ID, Total = 80.91 }
                );
            SaveChanges(context);

            context.OrderItems.AddOrUpdate(
                oi => oi.ID,
                new OrderItems { ID = 1, EquipmentID = 1, OrderID = 1, Quantity = 5, Total = 54.95 },
                new OrderItems { ID = 2, EquipmentID = 1, OrderID = 2, Quantity = 1, Total = 10.99 },
                new OrderItems { ID = 3, EquipmentID = 2, OrderID = 2, Quantity = 2, Total = 17.98 },
                new OrderItems { ID = 4, EquipmentID = 2, OrderID = 3, Quantity = 9, Total = 80.91 }
                );

            SaveChanges(context);
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
                ); // Add the original exception as the innerException
            }
        }

        public byte[] gifImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream()) {
                imageIn.Save(ms, ImageFormat.Gif);
                return ms.ToArray();
            }
        }

        public byte[] jpegImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream()) {
                imageIn.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        private int[] equipmentIDFinder(int id, ApplicationDbContext context)
        {
            List<Equipment> equipment = new List<Equipment>();
            using (context) {
                equipment = context.Equipment.ToList();
            }
            var equipmentIDs = new int[equipment.Count];
            for (int i = 0; i < equipment.Count - 1; i++) {
                equipmentIDs[i] = equipment[i].ID;
            }
            return equipmentIDs;

        }

        private string[] userIDFinder(int id, ApplicationDbContext context)
        {
            List<ApplicationUser> users = new List<ApplicationUser>();
            using (ApplicationDbContext db = new ApplicationDbContext()) {
                users = db.Users.ToList();
            }
            var userIDs = new string[users.Count];
            for (int i = 0; i < users.Count; i++) {
                userIDs[i] = users[i].Id;
            }
            return userIDs;
        }

        private int[] equipmentTypeIDFinder(int id, ApplicationDbContext context)
        {
            List<EquipmentTypes> types = new List<EquipmentTypes>();
            using (ApplicationDbContext db = new ApplicationDbContext()) {
                types = db.EquipmentTypes.ToList();
            }
            var typeIDs = new int[types.Count];
            for (int i = 0; i < types.Count; i++) {
                typeIDs[i] = types[i].ID;
            }
            return typeIDs;
        }

        private int[] equipmentRarityIDFinder(int id, ApplicationDbContext context)
        {
            List<EquipmentRarities> rarities = new List<EquipmentRarities>();
            using (ApplicationDbContext db = new ApplicationDbContext()) {
                rarities = db.EquipmentRarities.ToList();
            }
            var rarityIDs = new int[rarities.Count];
            for (int i = 0; i < rarities.Count; i++) {
                rarityIDs[i] = rarities[i].ID;
            }
            return rarityIDs;
        }

        private int[] postIDFinder(int id, ApplicationDbContext context)
        {
            List<Posts> posts = new List<Posts>();
            using (ApplicationDbContext db = new ApplicationDbContext()) {
                posts = db.Posts.ToList();
            }
            var postIDs = new int[posts.Count];
            for (int i = 0; i < posts.Count; i++) {
                postIDs[i] = posts[i].ID;
            }
            return postIDs;
        }

        private int[] paymentMethodIDFinder(int id, ApplicationDbContext context)
        {
            List<PaymentMethods> paymentMethods = new List<PaymentMethods>();
            using (ApplicationDbContext db = new ApplicationDbContext()) {
                paymentMethods = db.PaymentMethods.ToList();
            }
            var pmIDs = new int[paymentMethods.Count];
            for (int i = 0; i < paymentMethods.Count; i++) {
                pmIDs[i] = paymentMethods[i].ID;
            }
            return pmIDs;
        }
    }
}
