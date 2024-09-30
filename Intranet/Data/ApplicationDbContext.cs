using Intranet.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Core.Operations;
using System.Security.Cryptography;

namespace Intranet.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {

        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            var Admin_RoleId = Guid.NewGuid().ToString();
            var User_RoleId = Guid.NewGuid().ToString();

            builder.Entity<IdentityRole>().HasData(
              new { Id = Admin_RoleId, Name = "Admin", NormalizedName = "ADMIN" },
              new { Id = User_RoleId.ToString(), Name = "User", NormalizedName = "USER" }
            );

            ////create user
            //var user = new ApplicationUser
            //{
            //    UserName = "System@moshimoshi.co.th",
            //    NormalizedUserName = "SYSTEM@MOSHIMOSHI.CO.TH",
            //    Email = "System@moshimoshi.co.th",
            //    NormalizedEmail = "SYSTEM@MOSHIMOSHI.CO.TH",                
            //    FirstNameEN = "Admin",
            //    LastNameEN = "Admin",
            //    FirstNameTH = "แอดมิน",
            //    LastNameTH = "แอดมิน",
            //    PositionId = null,
            //    CreateBy = "Admin Admin",
            //    CreateOn = DateTime.Now,                
            //    Active = true,
            //    LockoutEnabled = true,
            //};

            ////set user password
            //PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>();
            //user.PasswordHash = ph.HashPassword(user, "Moshi#2023");

            //seed user
            //builder.Entity<ApplicationUser>().HasData(listuser);

            ////set user role to admin
            //builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            //{
            //    RoleId = Admin_RoleId,
            //    UserId = user.Id,
            //});

            var listuser = new List<ApplicationUser>
            {
                new ApplicationUser {
                UserName = "System@moshimoshi.co.th",
                NormalizedUserName = "SYSTEM@MOSHIMOSHI.CO.TH",
                Email = "System@moshimoshi.co.th",
                NormalizedEmail = "SYSTEM@MOSHIMOSHI.CO.TH",
                FirstNameEN = "Admin",
                LastNameEN = "Admin",
                FirstNameTH = "แอดมิน",
                LastNameTH = "แอดมิน",
                PositionId = null,
                CreateBy = "Admin Admin",
                CreateOn = DateTime.Now,
                Active = true,
                LockoutEnabled = true
                },
                new ApplicationUser {
                UserName = "comsec@moshimoshi.co.th",
                NormalizedUserName = "COMSEC@MOSHIMOSHI.CO.TH",
                Email = "comsec@moshimoshi.co.th",
                NormalizedEmail = "COMSEC@MOSHIMOSHI.CO.TH",                
                FirstNameEN = "Moshi",
                LastNameEN = "Moshi",
                FirstNameTH = "Moshi",
                LastNameTH = "Moshi",
                PositionId = null,
                CreateBy = "Admin Admin",
                CreateOn = DateTime.Now,
                Active = true,
                LockoutEnabled = true
                }
            };

            foreach (var item in listuser)
            {
                //set user password
                PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>();
                item.PasswordHash = ph.HashPassword(item, "Moshi#2023");

                //seed user
                builder.Entity<ApplicationUser>().HasData(item);


                if (item.UserName == "System@moshimoshi.co.th")
                {
                    //set user role to admin
                    builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
                    {
                        RoleId = Admin_RoleId,
                        UserId = item.Id,
                    });
                }
                else
                {
                    //set user role to admin
                    builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
                    {
                        RoleId = User_RoleId,
                        UserId = item.Id,
                    });
                }
            }
        }
        public DbSet<MenuGroup> MenuGroups { get; set; }
        public DbSet<UserMenuGroup> UserMenuGroups { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Department1> Department1s { get; set; }
        public DbSet<Department2> Department2s { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<LocationGroup> LocationGroups { get; set; }
        public DbSet<PasswordHistory> PasswordHistories { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<ShopGroup> ShopGroups { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<ShopPositionGroup> ShopPositionGroups { get; set; }
        public DbSet <HubConnection> HubConnections { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<DirectoryGroup> DirectoryGroups { get; set; }
        public DbSet<CategoryComplaint> CategoryComplaints { get; set; }
        public DbSet<ComplaintFrom> ComplaintFroms { get; set; }
        public DbSet<Complaint> Complaints { get; set; }

        //Log
        public DbSet<Log_MenuGroup> Log_MenuGroups { get; set; }
        public DbSet<Log_UserMenuGroup> Log_UserMenuGroups { get; set; }
        public DbSet<Log_ApplicationUser> Log_ApplicationUsers { get; set; }
        public DbSet<Log_Attachment> Log_Attachments { get; set; }
        public DbSet<Log_Department1> Log_Department1s { get; set; }
        public DbSet<Log_Department2> Log_Department2s { get; set; }
        public DbSet<Log_Location> Log_Locations { get; set; }
        public DbSet<Log_LocationGroup> Log_LocationGroups { get; set; }
        public DbSet<Log_Position> Log_Positions { get; set; }
        public DbSet<Log_ShopGroup> log_ShopGroups { get; set; }
        public DbSet<Log_Section> Log_Sections { get; set; }
        public DbSet<Log_Shop> Log_Shops { get; set; }
        public DbSet<Log_ShopPositionGroup> Log_ShopPositionGroups { get; set; }
        public DbSet<Log_UserConnection> Log_UserConnections { get; set; }
        public DbSet<Log_Post> Log_Posts { get; set; }
        public DbSet<Log_Like> Log_Likes { get; set; }
        public DbSet<Log_Comment> Log_Comments { get; set; }
        public DbSet<Log_Media> Log_Medias { get; set; }
        public DbSet<Log_Notification> Log_Notifications { get; set; }
        public DbSet<Log_DirectoryGroup> Log_DirectoryGroups { get; set; }
        public DbSet<Log_CategoryComplaint> Log_CategoryComplaints { get; set; }
        public DbSet<Log_ComplaintFrom> log_ComplaintFroms { get; set; }
        public DbSet<Log_Complaint> Log_Complaints { get; set; }
    }
}
