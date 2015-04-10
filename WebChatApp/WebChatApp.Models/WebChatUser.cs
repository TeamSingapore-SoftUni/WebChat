namespace WebChat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Security.Claims;
    using System.Threading.Tasks;
    
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class WebChatUser : IdentityUser
    {
        private ICollection<Message> sentMessages;
        private ICollection<Message> receivedMessages;
        private ICollection<Chatroom> chatrooms;

        public WebChatUser()
        {
            this.sentMessages = new HashSet<Message>();
            this.receivedMessages = new HashSet<Message>();
            this.chatrooms = new HashSet<Chatroom>();
        }

        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string FullName { get; set; }

        public string ImageDataURL { get; set; }
     
        [InverseProperty("Sender")]
        public virtual ICollection<Message> SentMessages
        {
            get { return this.sentMessages; }
            set { this.sentMessages = value; }
        }

        [InverseProperty("Receiver")]
        public virtual ICollection<Message> ReceivedMessages
        {
            get { return this.receivedMessages; }
            set { this.receivedMessages = value; }
        }

        public virtual ICollection<Chatroom> Chatrooms
        {
            get { return this.chatrooms; }
            set { this.chatrooms = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<WebChatUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }     
    }
}
