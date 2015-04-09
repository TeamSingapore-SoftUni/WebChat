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
    public class MessagesController : ApiController
    {
        public MessagesController()
            :base(new WebChatData())
        {
        }

        public MessagesController(IWebChatData data)
            :base(data)
        {
        }

        // GET api/messages
        public IHttpActionResult Get()
        {
            // TODO: Create view models for the response.
            var currentUser =
                this.data.Users.Find(HttpContext.Current.User.Identity.GetUserId());
            var response = new
                           {
                               SentMessages = currentUser.SentMessages.Select(m => m.Id),
                               ReceivedMessages = currentUser.ReceivedMessages.Select(m => m.Id)
                           };
            

            return this.Ok(response);
        }

        // GET api/messages/5
        public IHttpActionResult GetByUserId(Guid id)
        {
            var messageById = this.data.Messages.All().FirstOrDefault(m => m.Id == id);
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

            this.data.Messages.Add(message);
            this.data.SaveChanges();

            return this.Ok();
        }

        // POST api/messages/ToChatroom
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

            this.data.Messages.Add(message);
            this.data.SaveChanges();

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
