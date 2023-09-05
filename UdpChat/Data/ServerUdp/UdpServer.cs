using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UdpChat.Models;

namespace UdpChat.Data
{

    internal class UdpServer
    {
        public UdpClient ClientUdp { get; set; }

        private Dictionary<string, Action<string>> methodBox = null!;

        public UdpServer()
        {
            ClientUdp = new UdpClient();
        }

        public UdpServer(int port)
        {
            ClientUdp = new UdpClient(port);
        }

        public async Task WaitMessage()
        {
            while (true)
            {
                string message = await GetMessage();
                string messageMethodKey;

                var messageParser = MessageParcer(message);

                message = messageParser.Item1;
                messageMethodKey = messageParser.Item2;

               // methodBox[messageMethodKey].Invoke(message);

            }
        }

        private (string, string) MessageParcer(string message)
        {
            string MethodKey = null!;

            if (message.Length > 0)
            {
                MethodKey = message.Substring(0, message.IndexOf('{'));


                message = message.Substring(MethodKey.Length, message.Length);
            }

            return (message, MethodKey);
        }

        public async Task<String> GetMessage()
        {
            var result = await ClientUdp.ReceiveAsync();

            string message = Encoding.UTF8.GetString(result.Buffer);

            return message;
        }
    }
}
