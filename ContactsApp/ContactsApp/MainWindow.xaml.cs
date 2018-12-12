using ContactsApp.Classes;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ContactsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Contact> allContacts;
        public MainWindow()
        {
            InitializeComponent();

            allContacts = new List<Contact>();
            ReadDatabase();
        }

        private void NewContactButton_Click(object sender, RoutedEventArgs e)
        {
            NewContactWindow newContactWindow = new NewContactWindow();
            newContactWindow.ShowDialog();

            ReadDatabase();
        }

        public void ReadDatabase()
        {
            using (SQLite.SQLiteConnection connection = new SQLite.SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Contact>();
                //Order alphabatically
                allContacts = (connection.Table<Contact>().ToList()).OrderBy(c =>c.Name).ToList();
            }

            if(allContacts != null)
            {
                contactListView.ItemsSource = allContacts;
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox mySearchTextbox = (sender as TextBox);
            //Check for contacts which contains the same text from the search text by (with c=> working similar to foreach, in this case contact)
            var filteredContacts = allContacts.Where(c => c.Name.ToLower().Contains(mySearchTextbox.Text.ToLower())).ToList();

           /* var filteredContacts2 = (from c2 in allContacts
                                     where c2.Name.ToLower().Contains(mySearchTextbox.Text.ToLower())
                                     orderby c2.Email
                                     select c2).ToList();
           */

            contactListView.ItemsSource = filteredContacts;
        }

        private void ContactListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Contact selectedContact = (Contact)contactListView.SelectedItem;
            if (selectedContact != null)
            {
                UpdateContactWindow updateContactWindow = new UpdateContactWindow(selectedContact);
                updateContactWindow.ShowDialog();

                ReadDatabase();
            }
        }
    }
}
