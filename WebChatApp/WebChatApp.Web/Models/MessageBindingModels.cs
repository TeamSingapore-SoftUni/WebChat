namespace WebChat.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AddNewMessageBindingModel
    {
        [Required]
        [Display(Name = "Message")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "ReceiverId")]
        public string ReceiverId { get; set; }

        [Display(Name = "ChatroomId")]
        public Guid ChatroomId { get; set; } 
    }
}