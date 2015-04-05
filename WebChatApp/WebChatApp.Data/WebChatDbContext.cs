namespace WebChat.Data
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using WebChat.Models;
    using WebChat.Data.Migrations;

    public class WebChatDbContext : IdentityDbContext<WebChatUser>
    {
        public WebChatDbContext()
            : base("DefaultConnection", false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<WebChatDbContext, Configuration>());
        }

        public static WebChatDbContext Create()
        {
            return new WebChatDbContext();
        }
    }
}
