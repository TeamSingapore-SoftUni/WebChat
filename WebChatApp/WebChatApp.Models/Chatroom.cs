namespace WebChat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Chatroom
    {
        private ICollection<Message> messages;
        private ICollection<WebChatUser> users;

        public Chatroom()
        {
            this.Id = Guid.NewGuid();
            this.messages = new HashSet<Message>();
            this.users = new HashSet<WebChatUser>();
        }

        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ICollection<Message> Messages
        {
            get { return this.messages; }
            set { this.messages = value; }
        }

        public virtual ICollection<WebChatUser> Users
        {
            get { return this.users; }
            set { this.users = value; }
        }
    }
}
