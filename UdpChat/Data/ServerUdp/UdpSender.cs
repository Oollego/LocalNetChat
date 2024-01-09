using MahApps.Metro.IconPacks;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using UdpChat.Models;

namespace UdpChat.Data
{
    class UdpSender
    {
        

        public string CurrentIp { get; set; } = null!;
        public IPAddress IPaddress { get; set; } 
        public int Port { get; set; }

       
        public UdpSender()
        {
            IPaddress = null!;
            //ClientUdp = null!;
            //IpEndPoint = null!;
        }
        public UdpSender(string ipAddress, string port)
        {
            IPaddress = IPAddress.Parse(ipAddress);
            Port = System.Convert.ToInt32(port);
         
        }
     

        public async Task SendMessageAsync(string messageStr)
        {
  
            Message message = new Message();
            message.Text = messageStr;
            message.Date = DateTime.Now;
            message.IpAddress = CurrentIp;
            message.IsIncoming = true;
            message.GuidId = Guid.NewGuid();

            string messageResult = DataSerializer.SerializeMainMessage(message);

            await SendDataAsync(messageResult);
        }

        public async Task SendDataAsync(string message)
        {

           

            byte[] data = Encoding.UTF8.GetBytes(message);

            IPEndPoint IpEndPoint = new IPEndPoint(IPaddress, Port);

            //IPEndPoint IpEndPoint = new IPEndPoint(IPAddress.Parse( "127.0.0.1"), 5555);

            UdpClient ClientUdp = new UdpClient();

            await ClientUdp.SendAsync(data, IpEndPoint);
        }
        //public async Task SendDataAsync(string message, MethodKey methodKey)
        //{

        //    message = methodKey.ToString() + message;

        //    byte[] data = Encoding.UTF8.GetBytes(message);

        //    IPEndPoint IpEndPoint = new IPEndPoint(IPAddress, Port);

        //    //IPEndPoint IpEndPoint = new IPEndPoint(IPAddress.Parse( "127.0.0.1"), 5555);

        //    UdpClient ClientUdp = new UdpClient();

        //    await ClientUdp.SendAsync(data, IpEndPoint);
        //}

        public async Task SendMessageConfirmationAsync(Message message, IPAddress currentIP, int servicePort)
        {
            //Message receivedMessage = DataSerializer.DeserializeMainMessage(message);

            String serviceMesssageKey = "s";
            ServMessage servMessage = new ServMessage()
            {

                GuidId = message.GuidId,
                Date = DateTime.Now,
                IpAddress = currentIP.ToString(),
                MethodName = MethodKey.MessageConfirm
                

            };

            string messageResult = DataSerializer.SerializeServiceMessageAsync(servMessage);
            messageResult = serviceMesssageKey + messageResult;

            IPaddress = IPAddress.Parse(message.IpAddress);
            Port = servicePort;

            //ClientUdp = new UdpClient();
            // IPEndPoint ipPoint = new IPEndPoint(receivedMessage.IpAddress, servicePort);


            await SendDataAsync(messageResult);
            //await SendMessageAsync(messageResult, ipPoint);
        }

        //public async Task SendMessageConfirmationAsync(string message, IPAddress currentIP, int servicePort)
        //{
        //    Message receivedMessage = DataSerializer.DeserializeMessage(message);

        //    String methodKey = "ReceiveMessageConfirm";
        //    ServiceMessage servMesaage = new ServiceMessage()
        //    { 
                
        //        GuidId = receivedMessage.GuidId,
        //        CreationDate = DateTime.Now,
        //        IpAddress = currentIP

        //    };
         
        //    string messageResult = DataSerializer.SerializeServiceMessage(servMesaage);
        //    messageResult = methodKey + messageResult;

        //    IPAddress = receivedMessage.IpAddress;
        //    Port = servicePort;

        //    //ClientUdp = new UdpClient();
        //    // IPEndPoint ipPoint = new IPEndPoint(receivedMessage.IpAddress, servicePort);


        //    await SendDataAsync(messageResult, MethodKey.ReceiveMessageConfirm);
        //   //await SendMessageAsync(messageResult, ipPoint);
        //}

        //private async Task SendMessageAsync(string message, IPEndPoint ipPoint)
        //{
        //    byte[] data = Encoding.UTF8.GetBytes(message);

        //    int bytes = await ClientUdp.SendAsync(data, ipPoint);
        //}

    }
}
