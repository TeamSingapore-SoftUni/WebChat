namespace WebChat.Web.Controllers
{
    using System.Web.Http;
    using Microsoft.AspNet.Identity;
    using WebChat.Data;

    public class BaseApiController : ApiController
    {
        public BaseApiController(IWebChatData data)
        {
            this.Data = data;
        }

        protected IWebChatData Data { get; private set; }

        protected IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return this.InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        this.ModelState.AddModelError(string.Empty, error);
                    }
                }

                if (ModelState.IsValid)
                {
                    return this.BadRequest();
                }

                return this.BadRequest(this.ModelState);
            }

            return null;
        }
    }
}
