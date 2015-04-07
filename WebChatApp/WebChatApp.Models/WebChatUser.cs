namespace WebChat.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;
    
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class WebChatUser : IdentityUser
    {
        private ICollection<Message> messages;
        private ICollection<Chatroom> chatrooms;

        public WebChatUser()
        {
            this.messages = new HashSet<Message>();
            this.chatrooms = new HashSet<Chatroom>();
        }

        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string FullName { get; set; }

        public string ImageDataURL { get; set; }
     
        public virtual ICollection<Message> Messages
        {
            get { return this.messages; }
            set { this.messages = value; }
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
