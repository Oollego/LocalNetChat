using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UdpChat.Models;

namespace UdpChat.Data.ServerUdp
{
    internal class TCPServer
    {
        public int Port { get; set; }
        public Message ClientMessage { get; private set; }
        public string DirectoryPath { get; set; }

        public TCPServer(int port)
        {
            Port = port;
            ClientMessage = new Message();
            DirectoryPath = Directory.GetCurrentDirectory();
        }

        public async Task ServerStart()
        {

            TcpListener listener = new(System.Net.IPAddress.Any, Port);
            try
            {
                listener.Start();
                while (true)
                {
                    TcpClient tcpClient = await listener.AcceptTcpClientAsync();
                    if (tcpClient != null)
                    {
                        await Task.Run(async () => await ProcessTcpClientAsync(tcpClient));
                    }
                }
            }
            finally
            {
                listener.Stop();
            }
        }



        private async Task ProcessTcpClientAsync(TcpClient tcpClient)
        {

            try
            {


                using NetworkStream stream = tcpClient.GetStream();
                using BinaryReader reader = new BinaryReader(stream, Encoding.UTF8);
                using BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8, true);

                // Receive text
                String JsonText = reader.ReadString();

                if (JsonText != null) { ClientMessage = JsonSerializer.Deserialize<Message>(JsonText) ?? ClientMessage; }

                if (ClientMessage.IsFileAdded == true && !String.IsNullOrEmpty(ClientMessage.FileName))
                {
                    string newFileName = FileNameTransformation(ClientMessage.FileName);
                    if (newFileName == string.Empty)
                    {
                        ClientMessage.FileName = null!;
                        ClientMessage.IsFileAdded = false;
                        ClientMessage.FileLength = 0;
                        return;
                    }

                    writer.Write(ClientMessage.IsFileAdded);

                    byte[] buffer = new byte[ClientMessage.FileLength];
                    await stream.ReadAsync(buffer);

                    // reader.ReadBytes(message.FileLength);


                    String savedName = Path.Combine(DirectoryPath, newFileName);

                    using Stream filestream = System.IO.File.OpenWrite(savedName);
                    await stream.CopyToAsync(filestream);
                }
                else
                {
                    writer.Write(ClientMessage.IsFileAdded);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}", "Error");
            }

        }

        private string FileNameTransformation(string fileName)
        {
            if (fileName == null) return string.Empty;

            int dotPosition = fileName.LastIndexOf('.');
            if (dotPosition == -1)
            {
                return string.Empty;
            }

            String ext = fileName.Substring(dotPosition);
            string result = Guid.NewGuid() + ext;

            return result;
        }
    }
}
