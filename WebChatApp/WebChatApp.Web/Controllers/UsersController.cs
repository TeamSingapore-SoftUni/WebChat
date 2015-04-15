namespace WebChat.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;

    using WebChat.Data;
    using WebChat.Models;
    using WebChat.Web.Models.Account;
    using WebChat.Web.Models.Chatroom;

    [Authorize]
    [RoutePrefix("api/users")]
    public class UsersController : BaseApiController
    {
         private IWebChatData data;

        public UsersController()
            : this(new WebChatData())
        {
        }

        public UsersController(IWebChatData data)
            : base(data)
        {
            this.data = data;
        }


        // get api/users/GetByName?name={name}
        [HttpGet]
        [ActionName("GetByName")]
        public IHttpActionResult GetUserByName(string userName)
        {
            var user = this.data.Users.All().FirstOrDefault(u => u.UserName == userName);

            if (user == null)
            {
                return this.NotFound();
            }

            return this.Ok(new 
            {
                Id = new Guid(user.Id),
                Email = user.Email,
                UserName = user.UserName,
                FullName = user.FullName,
                ImageDataUrl = user.ImageDataUrl,
            });        
        }

        // get all user's chatrooms
        [HttpGet]
        [Route("chatrooms")]
        public IHttpActionResult GetUserChatrooms()
        {
            var currentUserId = HttpContext.Current.User.Identity.GetUserId();
            var currentUser = this.data.Users.All()
                .FirstOrDefault(u => u.Id == currentUserId);

            var chatrooms = currentUser.JoinedChatrooms.Select(c => new
            {
                Id = c.Id,
                Name = c.Name,
            });

            return this.Ok(chatrooms);
        }
    }

}