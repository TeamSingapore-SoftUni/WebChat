namespace WebChat.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Newtonsoft.Json;

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
        public string SenderId { get; set; }

        [JsonIgnore]
        public virtual WebChatUser Sender { get; set; }

        public string ReceiverId { get; set; }

        [JsonIgnore]
        public virtual WebChatUser Receiver { get; set; }

        public Guid? ChatroomId { get; set; }

        [JsonIgnore]
        public virtual Chatroom Chatroom { get; set; }
    }
}
