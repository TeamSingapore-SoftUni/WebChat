namespace WebChat.Web.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using WebChat.Data;
    using WebChat.Models;

    [Authorize]
    public class MessageController : ApiController
    {
        private IWebChatData data;

        public MessageController()
            : this(new WebChatData(new WebChatDbContext()))
        {
        }

        public MessageController(IWebChatData data)
        {
            this.data = data;
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
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
