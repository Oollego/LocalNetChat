using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UdpChat.Models;


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

        public static string SerializeMessage(Message message)
        {
            string serializedMessage = JsonSerializer.Serialize(message);
            return serializedMessage;
        }

        public static Message DeserializeMassage(string message)
        {
            
            Message? data = JsonSerializer.Deserialize<Message>(message);

            if (data == null)
            {
                data = new Message();
            }

            return data;
        }

        public static string SerializeServiceMessage(ServiceMessage message)
        {
            return JsonSerializer.Serialize(message);
        }

        public static ServiceMessage DeserializeServiceMassage(string message)
        {

            ServiceMessage? data = JsonSerializer.Deserialize<ServiceMessage>(message);

            if (data == null)
            {
                data = new ServiceMessage();
            }

            return data;
        }


    }
}
