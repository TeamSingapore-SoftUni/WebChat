namespace WebApiAungularWithPushNoti.Controllers
{
    using System;

    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;

    using WebChat.Data;
    using WebChat.Web.Controllers;

    public abstract class ApiControllerWithHub<THub> : BaseApiController
        where THub : IHub
    {
        protected ApiControllerWithHub(IWebChatData data)
            : base(data)
        {
        }

        Lazy<IHubContext> hub = new Lazy<IHubContext>(
            () => GlobalHost.ConnectionManager.GetHubContext<THub>());

        protected IHubContext Hub
        {
            get { return this.hub.Value; }
        }
    }
}