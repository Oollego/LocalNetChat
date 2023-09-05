using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdpChat.Data
{
    class MessagesMethods
    {
        private Dictionary<string, Action<string>> methodBox = null!;

        public MessagesMethods(string methodKey, string message)
        {

        }
    }
}
