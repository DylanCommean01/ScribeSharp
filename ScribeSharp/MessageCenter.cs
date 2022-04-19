using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScribeSharp
{
    public class MessageCenter
    {
        private List<Message> _messages;
        public MessageCenter(List<Message> messages)
        {
            _messages = messages;
        }

        public List<Message> Messages
        { get; }

        public bool RemoveMessage(Message message)
        {
            return Messages.Remove(message);
        }

        public void AddMessage(Message message)
        {
            Messages.Add(message);
        }
    }

}
