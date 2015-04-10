namespace WebChat.Web.Controllers
{
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Cookies;

    using WebChat.Data;
    using WebChat.Models;
    using WebChat.Web.Models.Account;

    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : BaseApiController
    {
        private WebChatUserManager userManager;

        public AccountController()
            : base(new WebChatData())
        {
            this.userManager = new WebChatUserManager(
                new UserStore<WebChatUser>(new WebChatDbContext()));
        }

        public AccountController(IWebChatData data)
            : base(data)
        {
        }

        public WebChatUserManager UserManager
        {
            get
            {
                return this.userManager;
            }
        }

        private IAuthenticationManager Authentication
        {
            get
            {
                return Request.GetOwinContext().Authentication;
            }
        }

        // TODO: 
        // POST api/Account/Login
        //[HttpPost]
        //[AllowAnonymous]
        //[Route("Login")]

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var user = new WebChatUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName,
                ImageDataUrl = model.ImageDataUrl
            };

            IdentityResult result = await this.UserManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return this.GetErrorResult(result);
            }


            return this.Ok();
        }

        // POST api/Account/ChangePassword
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            IdentityResult result =
                await this.UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                return this.GetErrorResult(result);
            }

            return this.Ok();
        }

        // GET api/Account/UserInfo
        [HttpGet]
        [Route("UserInfo")]
        public IHttpActionResult GetUserInfo()
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            // Validate the current user exists in the database
            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.All().FirstOrDefault(x => x.Id == currentUserId);
            if (currentUser == null)
            {
                return this.BadRequest("Invalid user token! Please login again!");
            }

            var userToReturn =
                new
                    {
                        currentUser.Id,
                        currentUser.UserName,
                        currentUser.FullName,
                        currentUser.Email,
                        ImageDataURL = currentUser.ImageDataUrl
                    };

            return this.Ok(userToReturn);
        }

        // POST api/Account/Logout
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            this.Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return this.Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.userManager != null)
            {
                this.userManager.Dispose();
                this.userManager = null;
            }

            base.Dispose(disposing);
        }
    }
}