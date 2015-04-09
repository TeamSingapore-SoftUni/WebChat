namespace WebChat.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using WebChat.Data.Repositories;
    using WebChat.Models;

    public class WebChatData : IWebChatData
    {
        private DbContext context;
        private IDictionary<Type, object> repositories;

        public WebChatData()
            :this(new WebChatDbContext())
        {
            
        }
        public WebChatData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<WebChatUser> Users
        {
            get { return this.GetRepository<WebChatUser>(); }
        }

        public IRepository<Chatroom> Chatrooms
        {
            get { return this.GetRepository<Chatroom>(); }
        }

        public IRepository<Message> Messages
        {
            get { return this.GetRepository<Message>(); }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(GenericRepository<T>), context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IRepository<T>)this.repositories[typeOfRepository];
        }
    }
}
