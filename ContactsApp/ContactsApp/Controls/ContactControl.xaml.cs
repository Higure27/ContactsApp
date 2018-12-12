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

namespace ContactsApp.Controls
{
    /// <summary>
    /// Interaction logic for ContactControl.xaml
    /// </summary>
    public partial class ContactControl : UserControl
    {
        /*private Contact contact;

        public Contact Contact
        {
            get { return contact; }
            set
            {
                contact = value;
                nameTextBlock.Text = contact.Name;
                phoneTextBlock.Text = contact.PhoneNum;
                emailTextBlock.Text = contact.Email;

            }
        }*/



        public Contact Contact
        {
            get { return (Contact)GetValue(ContactProperty); }
            set { SetValue(ContactProperty, value); }
        }


        //Provide a way to compute the value of a property based on the value of different things or different inputs
        // Using a DependencyProperty as the backing store for Contact.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContactProperty =
            DependencyProperty.Register("Contact", typeof(Contact), typeof(ContactControl), new PropertyMetadata(new Contact(),SetText));

        private static void SetText(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ContactControl control = d as ContactControl;

            if(control != null)
            {
                control.nameTextBlock.Text = ((Contact)e.NewValue).Name;
                control.phoneTextBlock.Text = ((Contact)e.NewValue).PhoneNum;
                control.emailTextBlock.Text = ((Contact)e.NewValue).Email;
            }
        }

        public ContactControl()
        {
            InitializeComponent();
        }
    }
}
