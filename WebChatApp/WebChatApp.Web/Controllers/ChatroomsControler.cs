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
    [RoutePrefix("api/Chatroom")]
    public class ChatroomController : BaseApiController
    {
        private IWebChatData data;

        public ChatroomController()
            : this(new WebChatData())
        {
        }

        public ChatroomController(IWebChatData data)
            : base(data)
        {
            this.data = data;
        }

        // POST api/Chatrooms
        [HttpPost]
        [ActionName("Create")]
        public IHttpActionResult CreateChatroom(ChatroomBindingModel model)
        {
            // get the user creating the chatroom
            //var userId = HttpContext.Current.User.Identity.GetUserId();
            //var user = this.data.Users.Find(userId);

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var isChatroomAlreadyExisting = this.data.Chatrooms.All().FirstOrDefault(c => c.Name == model.Name);
            if (isChatroomAlreadyExisting != null)
            {
                return this.BadRequest("Chatroom with the same name already exists");
            }

            var chatroom = new Chatroom
            {
                Name = model.Name
            };

            //chatroom.Users.Add(user);
            this.data.Chatrooms.Add(chatroom);
            this.data.SaveChanges();

            return this.Ok(
                new
                {
                    message = "Chatroom created successfully.",
                    chatroom = new ChatroomShortViewModel
                    {
                        Id = chatroom.Id,
                        Name = chatroom.Name
                    }
                });
        }

        // PUT api/values/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // get api/chatroom/GetByName?name={name}
        [HttpGet]
        [ActionName("GetByName")]
        public IHttpActionResult GetChatroomByName(string name)
        {
            var chatroom = this.data.Chatrooms.All().FirstOrDefault(c => c.Name == name);

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

            return this.NotFound();
        }

        // GET api/chatroom/GetById?id={id}
        [HttpGet]
        [ActionName("GetById")]
        public IHttpActionResult GetChatroomById(string id)
        {
            var idToGuid = new Guid(id);
            var chatroom = this.data.Chatrooms.All().FirstOrDefault(c => c.Id == idToGuid);

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

            return this.NotFound();
        }

        // GET api/chatroom/GetAll
        [HttpGet]
        [ActionName("GetAll")]
        public IHttpActionResult GetAllChatrooms()
        {
            var chatrooms = this.data.Chatrooms
                .All()
                .Select(c => new
                    {
                        ChannelId = c.Id,
                        ChannelName = c.Name,
                        UsersCount = c.Users.Count()
                    })
                .ToList();

            return this.Ok(chatrooms);
        }

        [HttpGet]
        [ActionName("GetCount")]
        public IHttpActionResult GetChatroomsCount()
        {
            var chatroomsCount = this.data.Chatrooms.All().Count();

            return this.Ok(chatroomsCount);
        }

        // GET /api/Chatroom/GetUsersByChatroom?name={name}
        [HttpGet]
        [ActionName("GetUsersByChatroom")]
        public IHttpActionResult GetUsersByChatroomName(string name)
        {
            var chatroom = this.data.Chatrooms.All().FirstOrDefault(c => c.Name == name);
            if (chatroom != null)
            {
                var usersList = 
                    new ChatroomShortViewModel
                        {
                            Id = chatroom.Id,
                            Name = chatroom.Name,
                            Users =
                                chatroom.Users.Select(
                                    u => new UserInfoShortViewModel { UserName = u.UserName })
                                .ToList()
                        };

                return this.Ok(usersList);
            }

            return this.NotFound();
        }

        // DELETE api/Chatroom/DelById?id={id}
        [HttpDelete]
        [ActionName("DelById")]
        public IHttpActionResult DeleteById(string id)
        {
            var chatroomForDeletion = this.data.Chatrooms
                .All()
                .FirstOrDefault(c => c.Id.ToString() == id);
            if (chatroomForDeletion != null)
            {
                this.data.Chatrooms.Delete(chatroomForDeletion);
                this.data.SaveChanges();
                return this.Ok("Chatroom deleted");
            }

            return this.NotFound();
        }

        // DELETE api/Chatroom/DelByName?name={name}
        [HttpDelete]
        [ActionName("DelByName")]
        public IHttpActionResult DeleteByName(string name)
        {
            var chatroomForDeletion = this.data.Chatrooms.All().FirstOrDefault(c => c.Name == name);
            if (chatroomForDeletion != null)
            {
                this.data.Chatrooms.Delete(chatroomForDeletion);
                this.data.SaveChanges();
                return this.Ok("Chatroom deleted");
            }

            return this.NotFound();
        }

        // POST api/Chatroom/Join?name={name}
        [HttpPost]
        [ActionName("Join")]
        public IHttpActionResult JoinChatroom(string name)
        {
            var chatroomForJoining = this.data.Chatrooms
                .All()
                .FirstOrDefault(c => c.Name == name);

            if (chatroomForJoining == null)
            {
                return this.NotFound();
            }

            var currentUserId = HttpContext.Current.User.Identity.GetUserId();
            var user = this.data.Users.Find(currentUserId);

            if (user == null || chatroomForJoining.Users.Contains(user))
            {
                return this.BadRequest();
            }

            chatroomForJoining.Users.Add(user);
            this.data.SaveChanges();
            return this.Ok();
        }

        // POST api/Chatroom/Leave?name={name}
        [HttpPost]
        [ActionName("Leave")]
        public IHttpActionResult LeaveChatroom(string name)
        {
            var chatroomForLeaving = this.data.Chatrooms
                .All()
                .FirstOrDefault(c => c.Name == name);

            if (chatroomForLeaving == null)
            {
                return this.NotFound();
            }

            var currentUserId = HttpContext.Current.User.Identity.GetUserId();
            var user = this.data.Users.Find(currentUserId);

            if (user == null || !chatroomForLeaving.Users.Contains(user))
            {
                return this.BadRequest();
            }

            chatroomForLeaving.Users.Remove(user);
            this.data.SaveChanges();
            return this.Ok();
        }
    }
}