using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScribeSharp
{
    public class Message
    {
        private string _text;
        private User _sender;
        private User _receiver;

        public Message(string text, User sender, User receiver)
        {
            _text = text;
            _sender = sender;
            _receiver = receiver;
        }

        public string Text { get; set; }
        public User Sender { get; set; }
        public User Receiver { get; set; }
    }
}
