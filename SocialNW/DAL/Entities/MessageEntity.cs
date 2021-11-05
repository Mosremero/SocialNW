using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNW.DAL.Entities
{
    public class MessageEntity
    {
        public int id { get; set; }
        public string content { get; set; }
        public int sender_id { get; set; }
        public int recipient_id { get; set; }
    }
}
