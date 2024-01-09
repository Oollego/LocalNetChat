using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace UdpChat.Models
{
    internal class ServerClient
    {
        public IPAddress ClientIP { get; }
        public int Port { get; }

        public string MacAddress { get; }

        ServerClient(IPAddress clientIP, int port, string macAddress)
        {
            ClientIP = clientIP;
            Port = port;
            MacAddress = macAddress;
        }
    }
}
