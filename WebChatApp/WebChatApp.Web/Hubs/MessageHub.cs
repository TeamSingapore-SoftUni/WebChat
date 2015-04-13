namespace WebChat.Web.Hubs
{
    using Microsoft.AspNet.SignalR;

    public class MessageHub : Hub
    {
        public void Send(string name, string message)
        {
            this.Clients.All.broadcastMessage(name, message);
        }
    }
}