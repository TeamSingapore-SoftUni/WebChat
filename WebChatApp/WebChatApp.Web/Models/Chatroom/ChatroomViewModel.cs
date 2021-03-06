﻿namespace WebChat.Web.Models.Chatroom
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using WebChat.Models;
    using WebChat.Web.Models.Account;   

    public class ChatroomViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

       // public ICollection<dynamic> Messages { get; set; }

        public ICollection<UserInfoViewModel> Users { get; set; }
    }
}