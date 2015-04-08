namespace WebChat.Web
{
    using Microsoft.AspNet.SignalR;

    public class ChatHub : Hub
    {
        public void Send(string name, string message)
        {
            this.Clients.All.broadcastMessage(name, message);
        }
    }
}