namespace WebChat.Web.Models.Chatroom
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class JoinChatroomBindingModel
    {
        public string ChatroomId { get; set; }

        public string ChatroomName { get; set; }
    }
}