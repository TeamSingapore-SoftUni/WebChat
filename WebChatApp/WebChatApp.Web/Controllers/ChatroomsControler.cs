namespace WebChat.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;

    using WebChat.Data;
    using WebChat.Models;
    using WebChat.Web.Models;
    using WebChat.Web.Models.Chatroom;
    using WebChat.Web.Models.Account;
 
    [Authorize]
    [RoutePrefix("api/Chatroom")]
    public class ChatroomController : ApiController
    {
        private IWebChatData data;

        public ChatroomController()
            :this (new WebChatData(new WebChatDbContext()))
        {
        }

        public ChatroomController(IWebChatData data)
        {
            this.data = data;
        }

        // get api/chatoom/byname?name={name}
        [HttpGet]
        [Route("byname")]
        public IHttpActionResult GetChatroomByName(string name)
        {
            var chatroom = this.data.Chatrooms.All().FirstOrDefault(c => c.Name == name);

            return this.Ok(new ChatroomViewModel
            {
                Id = chatroom.Id,
                Name = chatroom.Name,
                Users = chatroom.Users.Select(u => new UserInfoViewModel
                {
                    Id = new Guid(u.Id),
                    FullName = u.FullName,
                    UserName = u.UserName,
                    Email = u.Email,
                }).ToList()
            });
        }

        // GET api/chatoom/byId?id={id}
        [HttpGet]
        [Route("byId")]
        public IHttpActionResult GetChatroomById(string id)
        {
            var idToGuid = new Guid(id);
            var chatroom = this.data.Chatrooms.All().FirstOrDefault(c => c.Id == idToGuid);

            return this.Ok(new ChatroomViewModel
            {
                Id = chatroom.Id,
                Name = chatroom.Name,
                Users = chatroom.Users.Select(u => new UserInfoViewModel
                {
                    Id = new Guid(u.Id),
                    FullName = u.FullName,
                    UserName = u.UserName,
                    Email = u.Email,
                }).ToList()
            });
        }

        // POST api/Chatrooms
        [HttpPost]
        public IHttpActionResult CreateChatoom(ChatroomBindingModel model)
        {
            // get the use creating the chatroom
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var user = this.data.Users.Find(userId);

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var chatroom = new Chatroom
            {
                Name = model.Name
            };

            chatroom.Users.Add(user);
            this.data.Chatrooms.Add(chatroom);
            this.data.SaveChanges();

            return this.Ok(new
                {
                    message = "Chatroom created successfully.",
                    chatroom = new ChatroomViewModel 
                    {
                        Id = chatroom.Id,
                        Name = chatroom.Name,
                    }
                });
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
