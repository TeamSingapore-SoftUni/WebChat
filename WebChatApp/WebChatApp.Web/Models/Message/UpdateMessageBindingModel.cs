namespace WebChat.Web.Models.Message
{
    using System.ComponentModel.DataAnnotations;

    public class UpdateMessageBindingModel
    {
        [Required]
        [Display(Name = "Content")]
        public string Content { get; set; }
    }
}