namespace WebChat.Web.Models.Account
{
    using System;

    public class UserInfoViewModel
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public string ImageDataUrl { get; set; }

        public bool HasRegistered { get; set; }

        public string LoginProvider { get; set; }
    }
}