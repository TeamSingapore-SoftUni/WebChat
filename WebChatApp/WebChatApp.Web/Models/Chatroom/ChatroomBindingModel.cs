namespace WebChat.Web.Models.Chatroom
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ChatroomBindingModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}