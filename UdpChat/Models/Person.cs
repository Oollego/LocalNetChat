using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;


namespace UdpChat.Models
{
   
    internal class Person: INotifyPropertyChanged
    {

        public int Id { get; set; }

        string _name = null!;
        public string Name
        {
            get { return _name; }
            set { if (_name != value) { _name = value; INotifyPropertyChanged(); } }
        }
        

        
        string _surname = null!;
        public string Surname
        {
            get { return _surname; }
            set { if (_surname != value) { _surname = value; INotifyPropertyChanged(); } }
        }
  
        
       
        IPAddress _ipAddress = null!;
        public IPAddress IpAddress
        {
            get { return _ipAddress; }
            set { if (_ipAddress != value) { _ipAddress = value; INotifyPropertyChanged(); } }
        }


        public ObservableCollection<Message>? Messages { get; set; }

        void INotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }


  
   
}
