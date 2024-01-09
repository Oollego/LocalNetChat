using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdpChat.Models;

namespace UdpChat.Data
{
    class MessagesMethods
    {
        private Dictionary<string, Func<string, string>> methodBox = null!;
        public ObservableCollection<Person> PersonCollection { get; set; } = null!;

        private string _methodKey = string.Empty;
        private string _messageStr = string.Empty;

        public MessagesMethods(string methodKey, string message)
        {
            _messageStr = message;
            _methodKey = methodKey;
        }

        public string DoMethod()
        {
            return methodBox[_methodKey].Invoke(_messageStr);
        }

        //private string AddMessage() 
        //{
        //    string confirmMessage = string.Empty;

        //    Message message = DataSerializer.DeserializeMessage(_messageStr);


        //}



    }
}
