using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace RPGSite.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public List<Comments> Comments { get; set; }
        public List<Events> Events { get; set; }
        public List<Gallery> Gallery { get; set; }
        public List<Inventories> Inventories { get; set; }
        public List<Orders> Orders { get; set; }
        public List<Posts> Posts { get; set; }
        public List<WantedItem> WantedItems { get; set; }
        public List<OfferedItem> OfferedItems { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<EquipmentRarities> EquipmentRarities { get; set; }
        public DbSet<EquipmentTypes> EquipmentTypes { get; set; }
        public DbSet<Events> Events { get; set; }
        public DbSet<Gallery> Gallery { get; set; }
        public DbSet<Inventories> Inventories { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<PaymentMethods> PaymentMethods { get; set; }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Offers> Offers { get; set; }
        public DbSet<WantedItem> WantedItem { get; set; }
        public DbSet<OfferedItem> OfferedItem { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API can be used to configure some migrations
        }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

}