namespace WebChat.Web.Models.Message
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SendToChatroomBindingModel
    {
        [Required]
        [Display(Name = "Message")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "ChatroomId")]
        public Guid ChatroomId { get; set; }
    }
}