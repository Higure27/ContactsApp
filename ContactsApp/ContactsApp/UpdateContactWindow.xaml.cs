using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ContactsApp.Classes;
using SQLite;

namespace ContactsApp
{
    /// <summary>
    /// Interaction logic for UpdateContactWindow.xaml
    /// </summary>
    public partial class UpdateContactWindow : Window
    {
        Contact givenContact;
        public UpdateContactWindow(Contact contact)
        {
            InitializeComponent();
            givenContact = contact;
            nameTextBox.Text = givenContact.Name;
            emailTextBox.Text = givenContact.Email;
            phoneTextBox.Text = givenContact.PhoneNum;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {

            givenContact.Name = nameTextBox.Text;
            givenContact.Email = emailTextBox.Text;
            givenContact.PhoneNum = phoneTextBox.Text;

            //IDisposable which also calls the close method.
            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Contact>();
                connection.Update(givenContact);
            }

            this.Close();

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            //IDisposable which also calls the close method.
            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Contact>();
                connection.Delete(givenContact);
            }


            this.Close();
        }
    }
}
