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
        [MinLength(1, ErrorMessage = "Your message should contain at least 1 symbol.")]
        [MaxLength(500, ErrorMessage = "The message should be less than 500 symbols long.")]
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
