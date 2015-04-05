namespace WebChat.Data
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using WebChat.Data.Migrations;
    using WebChat.Models;

    public class WebChatDbContext : IdentityDbContext<WebChatUser>
    {
        public WebChatDbContext()
            : base("WebChatAppDb", false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<WebChatDbContext, Configuration>());
        }

        public static WebChatDbContext Create()
        {
            return new WebChatDbContext();
        }
    }
}
