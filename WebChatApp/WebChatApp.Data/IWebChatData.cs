namespace WebChat.Data
{
    using WebChat.Data.Repositories;
    using WebChat.Models;

    public interface IWebChatData
    {
        IRepository<WebChatUser> Users { get; }

        // IRepository<Chatroom> Chatrooms { get; }

        int SaveChanges();
    }   
}
