using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Topic_Manager.Model
{
    public class MessageBody
    {
        public string TopicName { get; set; }
        public string Payload { get; set; }
        public string Version { get; set; }
        public DateTime MessageDate { get; set; }
    }
}
