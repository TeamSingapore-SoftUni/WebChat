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
    using WebChat.Web.Models.Message;

    [Authorize]
    [RoutePrefix("api/Messages")]
    public class MessagesController : BaseApiController
    {
        public MessagesController()
            : base(new WebChatData())
        {
        }

        public MessagesController(IWebChatData data)
            : base(data)
        {
        }

        // GET api/messages
        [HttpGet]
        public IHttpActionResult Get()
        {
            // TODO: Create view models for the response.
            var currentUser =
                this.Data.Users.Find(HttpContext.Current.User.Identity.GetUserId());
            var response = 
                new
                {
                    SentMessages = currentUser.SentMessages.Select(m => m.Id),
                    ReceivedMessages = currentUser.ReceivedMessages.Select(m => m.Id)
                };
            

            return this.Ok(response);
        }

        // GET api/messages/5
        public IHttpActionResult GetByUserId(Guid id)
        {
            var messageById = this.Data.Messages.All().FirstOrDefault(m => m.Id == id);
            return this.Ok(messageById);
        }

        // POST api/messages/User
        [Route("User")]
        public IHttpActionResult PostMessageToUser(SendToUserBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var message = new Message()
            {
                Content = model.Content,
                SenderId = HttpContext.Current.User.Identity.GetUserId(),
                ReceiverId = model.ReceiverId,
                DateTime = DateTime.Now
            };

            this.Data.Messages.Add(message);
            this.Data.SaveChanges();

            return this.Ok();
        }

        // GET api/messages/Chatroom
        [Route("Chatroom")]
        [HttpGet]
        public IHttpActionResult GetMessagesByChatroom(Guid chatroomId)
        {
            var messages = this.Data.Messages
                .All()
                .Where(m => m.ChatroomId == chatroomId)
                .OrderBy(m => m.DateTime)
                .Select(m => new
                             {
                                 m.Id,
                                 m.Content,
                                 SenderName = m.Sender.UserName,
                                 m.DateTime
                             });

            return this.Ok(messages);
        }

        // POST api/messages/Chatroom
        [Route("Chatroom")]
        public IHttpActionResult PostMessageToChatroom(SendToChatroomBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var message = new Message()
            {
                Content = model.Content,
                SenderId = HttpContext.Current.User.Identity.GetUserId(),
                ChatroomId = model.ChatroomId,
                DateTime = DateTime.Now
            };

            this.Data.Messages.Add(message);
            this.Data.SaveChanges();

            return this.Ok();
        }

        // PUT api/messages/messageId
        [HttpPut]
        public void UpdateMessage(int id, [FromBody]string value)
        {
        }

        // DELETE api/message/messageId
        public void DeleteMessage(int id)
        {
        }
    }
}
