using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using UdpChat.Commands;
using UdpChat.Models;
using UdpChat.View.Windows;
using UdpChat.ViewModels.Base;
using UdpChat.Data;

namespace UdpChat.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {

        public ObservableCollection<Person> Persons { get; set; }


        // public int ListBoxSelectedIndex { get; set; }
        //public Person SelectedPersonItem { get; set; }

        private int _selectedPersonItem;
        public int SelectedPersonItem
        {
            get => _selectedPersonItem;

            set => Set(ref _selectedPersonItem, value);
        }


        private Person _selectedPerson = null!;
        public Person SelectedPerson
        {
            get => _selectedPerson;

            set => Set(ref _selectedPerson, value);
        }


        #region Commands
        public ICommand OpenContactEditorCommand { get; }
        private bool CanOpenContactEditorCommandCommandExecute(object p) => true;
        private void OnOpenContactEditorCommandExecuted(object p)
        {
            ContactsEditorWindow contactsEditor = new ContactsEditorWindow();
            contactsEditor.ShowDialog();


        }

        public ICommand AddContactCommand { get; }
        private bool CanAddContactCommandExecute(object p) => true;
        private void OnAddContactCommandExecuted(object p)
        {
            AddContactWindow contactWindow = new AddContactWindow();

            try
            {
                contactWindow.ShowDialog();

                if (contactWindow.IsCancel) return;

                Persons.Insert(0, new Person
                {
                    Name = contactWindow.PersonName,
                    Surname = contactWindow.PersonSurname,
                    IpAddress = contactWindow.PersonIpAddress,
                    Messages = new ObservableCollection<Message>()
                });
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        public ICommand DeleteContactAditorCommand { get; }
        private bool CanDeleteContactAditorCommandExecute(object p) => true;
        private void OnDeleteContactAditorCommandExecuted(object p)
        {
            Persons.RemoveAt(SelectedPersonItem);
        }


        public ICommand CloseWindowCommand { get; }
        private bool CanCloseWindowCommandExecute(object p) => true;
        private void OnCloseWindowCommandExecuted(object p)
        {
            FileSerializer fileSerializer = new FileSerializer();

            fileSerializer.SerializeData(Persons, "../../Persons.dat");
        }

        public ICommand EditContactCommand { get; }
        private bool CanEditContactCommandExecute(object p) => true;
        private void OnEditContactCommandExecuted(object p)
        {
            SubmitEditorWindow contactAditor = new SubmitEditorWindow();
            try
            {
                //  int index = (int)p;

                Person person = Persons.ElementAt(SelectedPersonItem);

                contactAditor.TextBoxName.Text = person.Name;
                contactAditor.TextBoxSurname.Text = person.Surname;
                contactAditor.TextBoxIpAddress.Text = person.IpAddress.ToString();

                contactAditor.ShowDialog();

                if (contactAditor.IsSubmited)
                {
                    Persons.ElementAt(SelectedPersonItem).Name = contactAditor.TextBoxName.Text;
                    Persons.ElementAt(SelectedPersonItem).Surname = contactAditor.TextBoxSurname.Text;
                    Persons.ElementAt(SelectedPersonItem).IpAddress = IPAddress.Parse(contactAditor.TextBoxIpAddress.Text);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

        }

        private CollectionViewSource _personViewSource = new CollectionViewSource();


        public ICollectionView PersonViewSource => _personViewSource.View;

        private string _personFiltredText = null!;
        public string PersonFiltredText
        {
            get => _personFiltredText;

            set
            {
                if(!Set(ref _personFiltredText, value)) return;
                _personViewSource.View.Refresh();
            }
        }
        #endregion
        private void OnPersonFiltred(object sender, FilterEventArgs e)
        {
            if(!(e.Item is Person person))
            {
                e.Accepted = false; 
                return;
            }

            var filter_text = _personFiltredText;

            if (string.IsNullOrWhiteSpace(filter_text)) return;

            if(person.Name is null || person.Surname is null || person.IpAddress is null) 
            {
                e.Accepted = false;
                return;
            }

            if (person.Name.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;
            if (person.Surname.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;
            if (person.IpAddress.ToString().Contains(filter_text)) return;

            e.Accepted = false;

        }
        public MainWindowViewModel()
        {
            #region Commands
            CloseWindowCommand = new LambdaCommand(OnCloseWindowCommandExecuted, CanCloseWindowCommandExecute);
            DeleteContactAditorCommand = new LambdaCommand(OnDeleteContactAditorCommandExecuted, CanDeleteContactAditorCommandExecute);
            AddContactCommand = new LambdaCommand(OnEditContactCommandExecuted, CanEditContactCommandExecute);
            AddContactCommand = new LambdaCommand(OnAddContactCommandExecuted, CanAddContactCommandExecute);
            OpenContactEditorCommand = new LambdaCommand(OnOpenContactEditorCommandExecuted, CanOpenContactEditorCommandCommandExecute);
            EditContactCommand = new LambdaCommand(OnEditContactCommandExecuted, CanEditContactCommandExecute);
            #endregion
            //var messages1 = Enumerable.Range(1, 8).Select(i => new Message() { Date = DateTime.Now, Text = $"Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text {i}" });
            //var messages2 = Enumerable.Range(1, 8).Select(i => new Message() { Date = DateTime.Now, Text = $"Text Text Text Text Text Text Text Text Text Text {i}", IsIncoming = true });
            //var messages = messages1.Union(messages2);

            //var persons = Enumerable.Range(1, 16).Select(i => new Person()
            //{
            //    IpAddress = IPAddress.Parse($"192.168.1.{i}"),
            //    Name = $"Name {i}",
            //    Surname = $"Surname {i}",
            //    Messages = new ObservableCollection<Message>(messages)

            //});
            //#FF47D41D

            FileSerializer fileSerializer = new FileSerializer();

            //fileSerializer.SerializeData(persons, "../../Persons.dat");

            Persons = fileSerializer.DeserializeData("../../Persons.dat");

           
            //Persons = new ObservableCollection<Person>(persons);

            _personViewSource.Source = Persons;

            _personViewSource.Filter += OnPersonFiltred;

        }


    }
}
