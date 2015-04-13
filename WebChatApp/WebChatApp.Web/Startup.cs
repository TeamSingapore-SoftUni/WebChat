namespace WebChat.Web
{
    using System.Web.Routing;

    using Microsoft.AspNet.SignalR;

    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}