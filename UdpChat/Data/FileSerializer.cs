using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using UdpChat.Models;

namespace UdpChat.Data
{
    class FileSerializer
    {
        public void SerializeData(IEnumerable<Person> persons, string path)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                writer.Write(persons.Count());

                foreach (Person person in persons)
                {

                    writer.Write(person.Name);
                    writer.Write(person.Surname);
                    writer.Write(person.IpAddress.ToString());

                    if (person.Messages is not null)
                    {
                        writer.Write(person.Messages.Count);

                        foreach (Message message in person.Messages)
                        {
                            string messageStr = DataSerializer.SerializeMessage(message);
                            writer.Write(messageStr);
                        }
                    }
                }

            }

        }

        public ObservableCollection<Person> DeserializeData(string path)
        {
            ObservableCollection<Person> persons = new ObservableCollection<Person>();

            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                int personsCount = reader.ReadInt32();

                for (int j = 0; j < personsCount; j++)
                { 

                    Person person = new Person();

                    person.Name = reader.ReadString();
                    person.Surname = reader.ReadString();
                    person.IpAddress = IPAddress.Parse(reader.ReadString());

                    int messagesCount = reader.ReadInt32();

                    person.Messages = new ObservableCollection<Message>();

                    for (int i = 0; i < messagesCount; i++)
                    {
                        string messageStr = reader.ReadString();

                        person.Messages.Add(DataSerializer.DeserializeMassage(messageStr));
                    }

                    persons.Add(person);
                }

            }

            return persons;

        }
    }
}

