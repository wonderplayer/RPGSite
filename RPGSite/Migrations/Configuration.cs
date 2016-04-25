namespace RPGSite.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Security.Policy;
    internal sealed class Configuration : DbMigrationsConfiguration<RPGSite.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RPGSite.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var passwordHash = new PasswordHasher();
            string password = passwordHash.HashPassword("Test112");
            context.Roles.AddOrUpdate(
                r => r.Name,
                new IdentityRole { Name = "Admin" },
                new IdentityRole { Name = "User" }
                );

            context.Users.AddOrUpdate(
                u => u.UserName,
                new ApplicationUser { UserName = "Admin", Email = "admin@admin.com", PasswordHash = password },
                new ApplicationUser { UserName = "Roland", Email = "roland@roland.com", PasswordHash = password },
                new ApplicationUser { UserName = "Edgar", Email = "edgar@edgar.com", PasswordHash = password }
                );

            context.EquipmentRarities.AddOrUpdate(
                er => er.ID,
                new EquipmentRarities { ID = 1, Rarity = "Rare" },
                new EquipmentRarities { ID = 2, Rarity = "Epic" }
                );

            context.EquipmentTypes.AddOrUpdate(
                et => et.ID,
                new EquipmentTypes { ID = 1, Type = "Sword" },
                new EquipmentTypes { ID = 2, Type = "Shield" }
                );

            context.Equipment.AddOrUpdate(
                e => e.ID,
                new Equipment { ID = 1, Title = "Dragon sword", Description = "Sword form dragons skin", Price = 10.99, TypeID = 1, RarityID = 1 },
                new Equipment { ID = 2, Title = "Dragons shield", Description = "Shield form dragons skin", Price = 8.99, TypeID = 2, RarityID = 1 },
                new Equipment { ID = 1, Title = "Warriors magic sword", Description = "Sword of mighty magic warrior", Price = 20.99, TypeID = 1, RarityID = 2 }
                );
        }
    }
}
