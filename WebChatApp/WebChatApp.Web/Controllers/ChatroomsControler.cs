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
    using WebChat.Web.Models.Account;
    using WebChat.Web.Models.Chatroom;

    [Authorize]
    [RoutePrefix("api/Chatroom")]
    public class ChatroomController : BaseApiController
    {
        public ChatroomController()
            : base(new WebChatData())
        {
        }

        public ChatroomController(IWebChatData data)
            :base(new WebChatData())
        {
        }

        // get api/chatroom/byName?name={name}
        [HttpGet]
        [Route("byName")]
        public IHttpActionResult GetChatroomByName(string name)
        {
            var chatroom = this.Data.Chatrooms.All().FirstOrDefault(c => c.Name == name);

            if (chatroom != null)
            {
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

            return this.BadRequest();
        }

        // GET api/chatroom/byId?id={id}
        [HttpGet]
        [Route("byId")]
        public IHttpActionResult GetChatroomById(string id)
        {
            var idToGuid = new Guid(id);
            var chatroom = this.Data.Chatrooms.All().FirstOrDefault(c => c.Id == idToGuid);

            if (chatroom != null)
            {
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

            return this.BadRequest();
        }

        [HttpGet]
        [Route("All")]
        public IHttpActionResult GetAllChatrooms()
        {
            var chatrooms = this.Data.Chatrooms
                .All()
                .Select(c => new
                {
                    ChannelName = c.Name
                })
                .ToList();

            return this.Ok(chatrooms);
        }

        // POST api/Chatrooms
        [HttpPost]
        public IHttpActionResult CreateChatroom(ChatroomBindingModel model)
        {
            // get the use creating the chatroom
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var user = this.Data.Users.Find(userId);

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var chatroom = new Chatroom
            {
                Name = model.Name
            };

            chatroom.Users.Add(user);
            this.Data.Chatrooms.Add(chatroom);
            this.Data.SaveChanges();

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
