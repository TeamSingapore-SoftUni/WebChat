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

    [Authorize]
    [RoutePrefix("api/Messages")]
    public class MessagesController : ApiController
    {
        private IWebChatData data;

        public MessagesController()
            : this(new WebChatData(new WebChatDbContext()))
        {
        }

        public MessagesController(IWebChatData data)
        {
            this.data = data;
        }

        // GET api/messages
        public IQueryable Get()
        {
            return this.data.Messages.All();
        }

        // GET api/messages/5
        public IHttpActionResult GetById(Guid id)
        {
            var messageById = this.data.Messages.All().FirstOrDefault(m => m.Id == id);
            return this.Ok(messageById);
        }

        // POST api/messages
        public IHttpActionResult PostMessage(AddNewMessageBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var message = new Message()
            {
                Content = model.Content,
                //SenderId = HttpContext.Current.User.Identity.GetUserId(),
                DateTime = DateTime.Now,
                //ReceiverId = model.ReceiverId,
                ChatroomId = model.ChatroomId
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
