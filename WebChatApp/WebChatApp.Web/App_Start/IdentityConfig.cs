namespace WebChat.Web
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;

    using WebChat.Data;
    using WebChat.Models;

    public class WebChatUserManager : UserManager<WebChatUser>
    {
        public WebChatUserManager(IUserStore<WebChatUser> store)
            : base(store)
        {
        }

        public static WebChatUserManager Create(IdentityFactoryOptions<WebChatUserManager> options, IOwinContext context)
        {
            var manager = new WebChatUserManager(new UserStore<WebChatUser>(context.Get<WebChatDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<WebChatUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<WebChatUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
}
