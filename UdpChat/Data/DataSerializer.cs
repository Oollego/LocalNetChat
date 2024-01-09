using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using UdpChat.Models;
using UdpChat.Models.MessageModels;

namespace UdpChat.Data
{
    class DataSerializer
    {
        //public string SerializeData(IEnumerable<Person> persons, string path)
        //{
        //    foreach (var person in persons)
        //    {
        //        using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
        //        {
        //            writer.Write(person);
        //        }
        //    }


        //}

        //public ObservableCollection<Person> DeserializeData(string data)
        //{

        //}

        public static IMessage DeserializeMessage(string message)
        {
            IMessage result;

            if (message.First() == 's')
            {
                message = message.Skip(1).ToString() ?? string.Empty;

                result = DeserializeServiceMessage(message);
            }
            else
            {
                result = DeserializeMainMessage(message);
            }

            return result;
        }


        public static string SerializeMainMessage(Message message)
        {
            string serializedMessage = JsonSerializer.Serialize<Message>(message);
            return serializedMessage;
        }

        public static Message DeserializeMainMessage(string message)
        {
            
            Message? data = JsonSerializer.Deserialize<Message>(message);

            if (data == null)
            {
                data = new Message();
            }

            return data;
        }

        public static ServMessage DeserializeServiceMessage(string message)
        {

            ServMessage? data = JsonSerializer.Deserialize<ServMessage>(message);

            if (data == null)
            {
                data = new ServMessage();
            }

            return data;
        }

        public static string SerializeServiceMessageAsync(ServMessage message)
        {
            //string serializedMessage = null!;

            string? serializedMessage  = JsonSerializer.Serialize<ServMessage>(message);
          
            
            return serializedMessage;
        }

       


    }
}
