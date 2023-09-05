using MahApps.Metro.IconPacks;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UdpChat.Data
{
    class UdpSender
    {
        public UdpClient ClientUdp { get; set; }

        IPEndPoint IpEndPoint { get; set; } = null!;

        public UdpSender()
        {
            ClientUdp = new UdpClient();
        }

        public UdpSender(IPEndPoint ipEndPoint)
        {
            ClientUdp = new UdpClient();

            IpEndPoint = ipEndPoint;
        }

        public async void SendData(string message, MethodKey methodKey)
        {
            message = methodKey.ToString() + message;

            byte[] data = Encoding.UTF8.GetBytes(message);

            int bytes = await ClientUdp.SendAsync(data);
        }

    }
}
