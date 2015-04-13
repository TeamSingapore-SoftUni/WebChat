namespace WebChat.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Collections.Generic;
    using System.Linq;
    
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    
    using WebChat.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebChatDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }


        protected override void Seed(WebChatDbContext context)
        {
            if (context.Users.Any())
            {
                // Seed initial data only if the database is empty
                return;
            }

            var chatrooms = this.SeedChatrooms(context);
            var users = this.SeedWebChatUsers(context);
        }

        private IList<Chatroom> SeedChatrooms(WebChatDbContext context)
        {
            var chatrooms = new List<Chatroom>();
            var chatroomNames = new List<string>
            {
                "Front-End Development", "Back-End Development", "Web Development", "C#", "Java", "JavaScript"
            };

            foreach (var chatroomName in chatroomNames)
            {
                var chatroom = new Chatroom() { Name = chatroomName, Messages = null, Users = null};
                context.Chatrooms.Add(chatroom);
                chatrooms.Add(chatroom);
            }

            context.SaveChanges();

            return chatrooms;
        }

        private IList<WebChatUser> SeedWebChatUsers(WebChatDbContext context)
        {
            var usernames = new string[] { "admin", "maria", "peter", "kiro", "didi" };

            var users = new List<WebChatUser>();
            var userStore = new UserStore<WebChatUser>(context);
            var userManager = new UserManager<WebChatUser>(userStore);
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 2,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            foreach (var username in usernames)
            {
                var name = username[0].ToString().ToUpper() + username.Substring(1);
                var user = new WebChatUser
                {
                    UserName = username,
                    FullName = name,
                    Email = username + "@gmail.com",
                    ImageDataUrl = null
                };

                var password = username;
                var userCreateResult = userManager.Create(user, password);
                if (userCreateResult.Succeeded)
                {
                    users.Add(user);
                }
                else
                {
                    throw new Exception(string.Join("; ", userCreateResult.Errors));
                }
            }

            context.SaveChanges();

            return users;
        }
    }
}
