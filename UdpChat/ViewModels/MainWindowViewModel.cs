﻿using System;
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
using System.Threading.Tasks;
using UdpChat.Data.Entities;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using UdpChat.Models.MessageModels;
using Microsoft.Win32;

namespace UdpChat.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private readonly DataContext _dataContext = null!;
        public ObservableCollection<ContactModel> _contacts { get; set; }

       
        

        public string _sendMessageText = null!;   // poprobivat sdelat obichnoe svoistvo
        public string SendMessageText
        {
            get => _sendMessageText;

            set => Set(ref _sendMessageText, value);
        }


        // public int ListBoxSelectedIndex { get; set; }
        //public Person SelectedPersonItem { get; set; }

        private int _selectedPersonItem;
        public int SelectedPersonItem
        {
            get => _selectedPersonItem;

            set =>  Set(ref _selectedPersonItem, value);
           
        }


        private ContactModel _selectedPerson = null!;
        public ContactModel SelectedPerson
        {
            get => _selectedPerson;

            set => Set(ref _selectedPerson, value);
        }


        #region Commands

        //public ICommand MessagesOfContactChangeCommand { get; }
        //private bool CanMessagesOfContactChangeCommandExecute(object p) => true;
        //private void OnMessagesOfContactChangeCommandExecuted(object p)
        //{
        //    ContactModel? selectedContact = p as ContactModel;
        //    if (selectedContact == null) { return; }

        //    var _mes = _dataContext.Messages.Select(m => m.ContactId == selectedContact.Id);

        //}

        public ICommand OpenMainSettingsCommand { get; }
        private bool CanOpenMainSettingsCommandExecute(object p) => true;
        private void OnOpenMainSettingsCommandExecuted(object p)
        {
            SettingsWindow mainSettings = new SettingsWindow();
            mainSettings.ShowDialog();

            //_contacts = (ObservableCollection<Person>)_personViewSource.Source;

        }

        public ICommand OpenContactEditorCommand { get; }
        private bool CanOpenContactEditorCommandExecute(object p) => true;
        private void OnOpenContactEditorCommandExecuted(object p)
        {
            ContactsEditorWindow contactsEditor = new ContactsEditorWindow();
            contactsEditor.ShowDialog();
            
            //_contacts = (ObservableCollection<Person>)_personViewSource.Source;

        }



        
        public ICommand AddAttachmentCommand { get; }
        private bool CanAddAttachmentCommandExecute(object p) => true;
        private void OnAddAttachmentCommandExecuted(object p)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {

            }
                //txtEditor.Text = File.ReadAllText(openFileDialog.FileName);
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
                ContactModel contact = new()
                {
                    Id = Guid.NewGuid(),
                    Name = contactWindow.PersonName,
                    Surname = contactWindow.PersonName,
                    IpAddress = contactWindow.PersonIpAddress.ToString(),
                    IsAvatarAdded = false,
                    NotReadedMessage = 0

                };
                _dataContext.Add(contact);
                _dataContext.SaveChangesAsync();
 
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
            ContactModel? selectedContact = p as ContactModel;
            if (selectedContact == null) { return; }

            _dataContext.Remove(selectedContact!);
            _dataContext.SaveChangesAsync();
        }


        public ICommand CloseWindowCommand { get; }
        private bool CanCloseWindowCommandExecute(object p) => true;
        private void OnCloseWindowCommandExecuted(object p)
        {
            //FileSerializer fileSerializer = new FileSerializer();

            //fileSerializer.SerializeData(_contacts, "../../_contacts.dat");
        }

        public ICommand OpenWindowCommand { get; }
        private bool CanOpenWindowCommandExecute(object p) => true;
        private void OnOpenWindowCommandExecuted(object p)
        {
            //UdpServer udpServer = new UdpServer(5554, IPAddress.Parse("127.0.0.1"), 5555);
            //udpServer.PersonCollection = _contacts;
            
            //Task.Run(async() => { await udpServer.WaitMessageAsync(); }) ;
        }

        public ICommand SendMessageCommand { get; }
        private bool CanSendMessageCommandExecute(object p) => true;
        private void OnSendMessageCommandExecuted(object p)
        {
            try
            {
                ContactModel? contactModel = p as ContactModel;
                if (contactModel == null) { return; }

                MessageModel message = new();
                //message.Id = Guid.NewGuid();
                message.IpAddress = contactModel.IpAddress;
                message.Text = SendMessageText;
                message.Date = DateTime.Now;
                message.IsIncoming = false;
                message.FileName = "";
                message.ContactId = contactModel.Id;
                contactModel.Messages.Add(message);
                
                _dataContext.SaveChanges();
                SendMessageText = "";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }



        public ICommand EditContactCommand { get; }
        private bool CanEditContactCommandExecute(object p) => true;
        private void OnEditContactCommandExecuted(object p)
        {
            SubmitEditorWindow contactAditor = new SubmitEditorWindow();
            try
            {
                //  int index = (int)p;

                ContactModel person = _contacts.ElementAt(SelectedPersonItem);

                contactAditor.TextBoxName.Text = person.Name;
                contactAditor.TextBoxSurname.Text = person.Surname;
                contactAditor.TextBoxIpAddress.Text = person.IpAddress;

                contactAditor.ShowDialog();

                if (contactAditor.IsSubmited)
                {
                    _contacts.ElementAt(SelectedPersonItem).Name = contactAditor.TextBoxName.Text;
                    _contacts.ElementAt(SelectedPersonItem).Surname = contactAditor.TextBoxSurname.Text;
                    _contacts.ElementAt(SelectedPersonItem).IpAddress = contactAditor.TextBoxIpAddress.Text;
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
            if(!(e.Item is ContactModel person))
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
        public MainWindowViewModel(DataContext dataContext)
        {
            #region Commands
            AddAttachmentCommand = new LambdaCommand(OnAddAttachmentCommandExecuted, CanAddAttachmentCommandExecute);
            //MessagesOfContactChangeCommand = new LambdaCommand(OnMessagesOfContactChangeCommandExecuted, CanMessagesOfContactChangeCommandExecute);
            OpenMainSettingsCommand = new LambdaCommand(OnOpenMainSettingsCommandExecuted, CanOpenMainSettingsCommandExecute);
            OpenWindowCommand = new LambdaCommand(OnOpenWindowCommandExecuted, CanOpenWindowCommandExecute);
            SendMessageCommand = new LambdaCommand(OnSendMessageCommandExecuted, CanSendMessageCommandExecute);
            CloseWindowCommand = new LambdaCommand(OnCloseWindowCommandExecuted, CanCloseWindowCommandExecute);
            DeleteContactAditorCommand = new LambdaCommand(OnDeleteContactAditorCommandExecuted, CanDeleteContactAditorCommandExecute);
            //AddContactCommand = new LambdaCommand(OnEditContactCommandExecuted, CanEditContactCommandExecute);
            AddContactCommand = new LambdaCommand(OnAddContactCommandExecuted, CanAddContactCommandExecute);
            OpenContactEditorCommand = new LambdaCommand(OnOpenContactEditorCommandExecuted, CanOpenContactEditorCommandExecute);
            EditContactCommand = new LambdaCommand(OnEditContactCommandExecuted, CanEditContactCommandExecute);
            #endregion



            #region Testdata
            var messages1 = Enumerable.Range(1, 8).Select(i => new MessageModel() { Date = DateTime.Now, Text = $"Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text {i}" });
            var messages2 = Enumerable.Range(1, 8).Select(i => new MessageModel() { Date = DateTime.Now, Text = $"Text Text Text Text Text Text Text Text Text Text {i}", IsIncoming = true });
            var messages = messages1.Union(messages2);

            var persons = Enumerable.Range(1, 16).Select(i => new ContactModel()
            {
                IpAddress = $"127.0.0.{i}",
                Name = $"Name {i}",
                Surname = $"Surname {i}",
                Messages = new ObservableCollection<MessageModel>(messages)

            });
            #endregion
            //#FF47D41D


            //_contacts = new ObservableCollection<ContactModel>(persons);

            //_personViewSource.Source = _contacts;
            _dataContext = dataContext;
            _dataContext.Contacts.Include(m => m.Messages).LoadAsync();
            
            _contacts = _dataContext.Contacts.Local.ToObservableCollection();


            _personViewSource.Source = _contacts;
            _personViewSource.Filter += OnPersonFiltred;

        }


    }
}
