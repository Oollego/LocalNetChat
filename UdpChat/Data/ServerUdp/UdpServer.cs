using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UdpChat.Models;
using System.Windows;
using UdpChat.Models.MessageModels;

namespace UdpChat.Data
{

    internal class UdpServer
    {
        public UdpClient ClientUdp { get; set; }

        private IMessage _recievedMessage = null!;
        public IMessage RecievedMessage
        {
            get => _recievedMessage;

            set
            {
                if (value is null) return;
                _recievedMessage = value;
                MessageHandler(value);
            }
        }

        IPAddress CurrentIp { get; set; }

        public int ListenPort { get; }

        public int ServicePort { get; set; }
        public ObservableCollection<Person> PersonCollection { get; set; } = null!;

        //private Dictionary<string, Action<string>> methodBox = null!;
      





        //public UdpServer()
        //{
        //    ClientUdp = new UdpClient();
        //    methodBox.Add("Message", ReceiveMessage);
        //    methodBox.Add("ReceiveMessageConfirm", ReceiveMessageConfirmAsync);
        //}

        public UdpServer(int port, IPAddress currentIp, int servicePort)
        {
            ClientUdp = new UdpClient(port);
            ListenPort = port;
            CurrentIp = currentIp;
            ServicePort = servicePort;

        }

        public void MethodSelector(string MethodKey, string message)
        {
            switch(MethodKey)
            {
                case "Message":
                    {
                        //ReceiveMessage(message);
                        break;
                    }
                case "MessageConfirm":
                    {
                        MessageConfirmAsync(message);
                        break;
                    }
            }
        }

        //private IPAddress GetSenderIPaddress()
        //{
        //    IPEndPoint point = (IPEndPoint)ClientUdp.Client.RemoteEndPoint;
        //    return point.Address;
        //}

        public async Task WaitMessageAsync()
        {

            while (true)
            {
               
                string messageStr = await GetMessageAsync();

                IMessage messageObject = DataSerializer.DeserializeMessage(messageStr);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    RecievedMessage = messageObject;

                });


            }
        }

         
        public void ServerStart()
        {
            //await WaitMessageAsync();
            Task.Run(async () => { await WaitMessageAsync(); });

            //if (RecievedMessage is Message message)
            //{
            //    UdpSender udpSender = new UdpSender();

            //    await udpSender.SendMessageConfirmationAsync(message, CurrentIp, ServicePort);
            //    AddMessageToPerson(message);

            //}
            //if (RecievedMessage is ServMessage servMessage)
            //{
            //    MessageConfirmAsync(servMessage);
            //}
        }
        public void MessageHandler(IMessage message)
        {


            if (message is Message mainMessage)
            {
                UdpSender udpSender = new UdpSender();

               // Task.Run(async () => { await udpSender.SendMessageConfirmationAsync(mainMessage, CurrentIp, ServicePort); });
                udpSender.SendMessageConfirmationAsync(mainMessage, CurrentIp, ServicePort);

                AddMessageToPerson(mainMessage);
               // ReceiveMessage(message);

            }
            if (message is ServMessage servMessage)
            {
                // MessageConfirmAsync(servMessage);
            }
        }

        //public async Task WaitMessageAsync()
        //{

        //    while (true)
        //    {
        //        //Task.Run( () => { string message = await  GetMessageAsync(); });

        //        string messageStr = await GetMessageAsync();

        //        IMessage messageObject = DataSerializer.DeserializeMessage(messageStr);

        //        if(messageObject is Message message)
        //        {
        //            UdpSender udpSender = new UdpSender();

        //            await udpSender.SendMessageConfirmationAsync(message, CurrentIp, ServicePort);
        //            ReceiveMessage(message);

        //        }
        //        if (messageObject is ServMessage servMessage)
        //        {
        //           // MessageConfirmAsync(servMessage);
        //        }

        //    }
        //}

        //public async Task WaitMessageAsync()
        //{
        //    //methodBox.Add("Message", ReceiveMessage);
        //    //methodBox.Add("ReceiveMessageConfirm", ReceiveMessageConfirmAsync);
        //    while (true)
        //    {
        //        string message = await GetMessageAsync();
        //        //string messageMethodKey = message.Substring(0, message.IndexOf('{'));

        //        //var messageParser = MessageParcer(message);

        //        //string methodKey = message.Substring(0, message.IndexOf('{'));

        //        //try
        //        //{
        //        //    int lKey = messageMethodKey.Length;
        //        //    int rmes = message1.Length;
        //        //     message = message1.Substring(lKey, rmes-lKey);
        //        //}
        //        //catch(Exception ex)
        //        //{
        //        //    MessageBox.Show(ex.Message);
        //        //}
        //        //message = messageParser.Item1;
        //        //messageMethodKey = messageParser.Item2;

        //        if (messageMethodKey == "Message")
        //        {

        //            UdpSender udpSender = new UdpSender();
        //            await udpSender.SendMessageConfirmationAsync(message, CurrentIp, ServicePort);
        //        }
        //        MethodSelector(messageMethodKey, message);
        //        // methodBox[messageMethodKey].Invoke(message);

        //    }
        //}

        private void AddMessageToPerson(Message message)
        {
            Person person = PersonCollection.Where(i => i.IpAddress.ToString() == message.IpAddress.ToString()).First();

            if (person is not null)
            {
                //    Application.Current.Dispatcher.Invoke(() =>
                //    {

                 person.Messages.Add(message);
                //    });

            }


        }

        private void MessageConfirmAsync(string servMessage)
        {
            ServMessage receivedMessage = DataSerializer.DeserializeServiceMessage(servMessage);

            Person person = PersonCollection.Where(i => i.IpAddress.ToString() == receivedMessage.IpAddress).First();

            Message personMessage =  person.Messages.Where(i => i.GuidId == receivedMessage.GuidId).First();

            personMessage.IsReceived = true;

        }
             
        public async Task<string> GetMessageAsync()
        {
            var result = await ClientUdp.ReceiveAsync();

            string message = Encoding.UTF8.GetString(result.Buffer);

            return message;
        }
    }
}
