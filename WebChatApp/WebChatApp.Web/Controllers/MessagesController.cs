namespace WebChat.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;

    using WebApiAungularWithPushNoti.Controllers;

    using WebChat.Data;
    using WebChat.Models;
    using WebChat.Web.Models.Message;

    using WebChat.Web.Hubs;

    [Authorize]
    [RoutePrefix("api/Messages")]

    public class MessagesController : ApiControllerWithHub<MessageHub>
    {
        public MessagesController()
            : base(new WebChatData())
        {
        }

        public MessagesController(IWebChatData data)
            : base(data)
        {
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

        [Route("User")]
        public IHttpActionResult GetConversationWithUser(string userId)
        {
            var ownUser = this.Data.Users.Find(HttpContext.Current.User.Identity.GetUserId());
            var otherUser = this.Data.Users.Find(userId);
            var conversation =
                ownUser.SentMessages
                    .Where(m => m.ReceiverId == otherUser.Id)
                    .Union(otherUser.SentMessages.Where(m => m.ReceiverId == ownUser.Id))
                    .OrderBy(m => m.DateTime)
                    .Select(m => new
                                 {
                                     m.Id,
                                     m.Content,
                                     m.DateTime,
                                     SenderName = m.Sender.UserName,
                                     ReceiverName = m.Receiver.UserName
                                 });

            return this.Ok(conversation);
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
            this.Hub.Clients.All.broadcastMessage(HttpContext.Current.User.Identity.GetUserName(), message.Content, message.DateTime);

            return this.Ok();
        }

        // PUT api/messages/messageId
        [Route("")]
        [HttpPut]
        public IHttpActionResult UpdateMessage(Guid id, UpdateMessageBindingModel model)
        {
            var message = this.Data.Messages.Find(id);
            if (message.SenderId != HttpContext.Current.User.Identity.GetUserId())
            {
                return this.BadRequest("You cannot edit foreign messages.");
            }

            message.Content = model.Content;
            this.Data.Messages.Update(message);
            this.Data.SaveChanges();
            return this.Ok("Message updated successfully!");
        }
    }
}
