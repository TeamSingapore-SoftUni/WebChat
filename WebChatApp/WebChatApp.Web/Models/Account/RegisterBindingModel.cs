namespace WebChat.Web.Models.Account
{
   using System.ComponentModel.DataAnnotations;

   public class RegisterBindingModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "FullName")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "ImageDataURL")]
        [RegularExpression("^data:image/.*$", ErrorMessage = "The image data should be valid Data URL, e.g. data:image/jpeg;base64,iVBORw0KGgo...")]
        public string ImageDataURL { get; set; }
    }
}