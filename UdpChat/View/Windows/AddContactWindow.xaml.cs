using System;
using System.Net;
using System.Windows;


namespace UdpChat.View.Windows
{
    /// <summary>
    /// Interaction logic for AddContactWindow.xaml
    /// </summary>
    public partial class AddContactWindow : Window
    {
        public string PersonName { get; set; }
        public string PersonSurname { get; set; }
        public IPAddress PersonIpAddress { get; set; }
        public bool IsCancel { get; private set; }
        public AddContactWindow()
        {
            InitializeComponent();

            IsCancel = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            PersonName = TextBoxName.Text;
            PersonSurname = TextBoxSurname.Text;
            try
            {
                PersonIpAddress = IPAddress.Parse(TextBoxIpAddress.Text);
            }
            catch (Exception)
            {
                throw new Exception("Ip address in incorrect");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            IsCancel = true;
        }

       
    }
}
