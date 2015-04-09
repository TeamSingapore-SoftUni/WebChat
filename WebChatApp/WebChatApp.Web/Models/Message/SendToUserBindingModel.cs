namespace WebChat.Web.Models.Message
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SendToUserBindingModel
    {
        [Required]
        [Display(Name = "Message")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "ReceiverId")]
        public string ReceiverId { get; set; }
    }
}