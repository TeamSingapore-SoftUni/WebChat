namespace WebChat.Web.Models.Message
{
    using System.ComponentModel.DataAnnotations;

    public class SendToUserBindingModel
    {
        [Required]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "ReceiverId")]
        public string ReceiverId { get; set; }
    }
}