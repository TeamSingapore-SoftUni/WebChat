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

        public IDbSet<Message> Messges { get; set; }

        public IDbSet<Chatroom> Chatroom { get; set; }

        public static WebChatDbContext Create()
        {
            return new WebChatDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
