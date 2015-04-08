namespace WebChat.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Message
    {
        public Message()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Content { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public string UserId { get; set; }
 
        public virtual WebChatUser User { get; set; }

        public Guid ChatroomId { get; set; }

        public virtual Chatroom Chatroom { get; set; }
    }
}
