namespace WebChat.Data
{
    using WebChat.Data.Repositories;
    using WebChat.Models;

    public interface IWebChatData
    {
        IRepository<WebChatUser> Users { get; }

        IRepository<Chatroom> Chatrooms { get; }

        IRepository<Message> Messages { get; }

        int SaveChanges();
    }   
}
