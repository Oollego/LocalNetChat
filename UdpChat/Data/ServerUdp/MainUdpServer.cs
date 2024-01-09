using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Linq;
using UdpChat.Models;

namespace UdpChat.Data.ServerUdp
{
    internal class MainUdpServer
    {
        public int ServerPort { get; set; }

        public IPAddress ServerIP { get; set; }

        private List<ServerClient> ClientList { get; set; } = null!;

        MainUdpServer(int serverRXPort, int serverTXPort, IPAddress serverIp)
        {
            ServerPort = serverRXPort;
                                 
            ServerIP = serverIp;
            
        }

        public void ServerStart()
        {
            using var udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            IPEndPoint serverIp = new IPEndPoint(ServerIP, ServerPort);

            udpSocket.Bind(serverIp);
            byte[] data = new byte[1024];

            IPEndPoint remoteIp = new IPEndPoint(IPAddress.Any, 0);

            Task.Run(async () =>
            {
                var result = await udpSocket.ReceiveFromAsync(data, remoteIp);

                string message = Encoding.UTF8.GetString(data, 0, result.ReceivedBytes);



                //tut nujno zapustit method tcp dlya otpravky dannih clientu gde nahoditsya server
            });
        }

       

    }


}
