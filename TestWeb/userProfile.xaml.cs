using c_sharp_kursa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace TestWeb
{
    /// <summary>
    /// Interaction logic for UserProfile.xaml
    /// </summary>
    public partial class userProfile : UserControl
    {
        MainWindow mw;
        DatabaseConnection dbConn;
        string login;

        public userProfile(MainWindow mw, DatabaseConnection dbConn, string login)
        {
            this.mw = mw;
            this.dbConn = dbConn;
            this.login = login;
            InitializeComponent();
            LoadData(login);
        }

        public void LoadData(string login)
        {
            List<string> userData;
            string query = "SELECT Name, Lastname, Password, Email, Phone FROM Users WHERE Login='" + login + "'";
            userData = dbConn.ReadData(query);
            userNameTB.Text = userData[0];
            lastNameTB.Text = userData[1];
            password1PB.Password = userData[2];
            password2PB.Password = userData[2];
            emailTB.Text = userData[3];
            phoneTB.Text = userData[4];
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            string name = userNameTB.Text;
            string lastname = lastNameTB.Text;
            string email = emailTB.Text;
            string password1 = password1PB.Password;
            string password2 = password2PB.Password;
            string phone = phoneTB.Text;

            if (checkEmail(email) & checkName(name) & checkPassword(password1, password2) & checkPhone(phone))
            {
                string query = "UPDATE Users SET Name='" + name + "', Lastname='" + lastname + "', Email='" + email + "', Password='" + password1 + "', Phone='" + phone + "' " +
                    "WHERE Login='" + login + "'";

                if (dbConn.WriteData(query))
                {
                    MessageBox.Show("User data was successfully modified", "Edit user data", MessageBoxButton.OK, MessageBoxImage.Information);
                    Window parent = Window.GetWindow(this);
                    parent.Close();
                }
            }
            else
                MessageBox.Show("Data you input is invalid. Please re-check.", "Edit user data", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public bool checkName(string name)
        {
            for (int i = 0; i < name.Length; i++)
            {
                if (char.IsDigit(name[i]))
                {
                    userNameTB.Background = Brushes.Red;
                    return false;
                }
            }

            if (name.Equals(""))
            {
                userNameTB.Background = Brushes.Red;
                return false;
            }
            userNameTB.Background = Brushes.White;
            return true;
        }

        public bool checkEmail(string email)
        {
            string pattern = @"([a-z]+)([@])([a-z]+)([.])([a-z]{2,3})";
            Regex regex = new Regex(pattern);

            if (!regex.IsMatch(email))
            {
                emailTB.Background = Brushes.Red;
                return false;
            }
            if (email.Equals(""))
            {
                emailTB.Background = Brushes.Red;
                return false;
            }
            emailTB.Background = Brushes.White;
            return true;
        }

        public bool checkPassword(string password1, string password2)
        {
            if (!password1.Equals("") && !password2.Equals(""))
                if (password1.Equals(password2))
                {
                    password1PB.Background = Brushes.White;
                    password2PB.Background = Brushes.White;
                    return true;
                }

            if (password1.Equals(""))
                password1PB.Background = Brushes.Red;

            if (password2.Equals(""))
                password2PB.Background = Brushes.Red;

            return false;
        }

        public bool checkPhone(string phone)
        {
            for (int i = 0; i < phone.Length; i++)
            {
                if (!char.IsDigit(phone[i]))
                {
                    phoneTB.Background = Brushes.Red;
                    return false;
                }
            }
            phoneTB.Background = Brushes.White;
            return true;
        }

        public bool checkLastName(string lastName)
        {
            for (int i = 0; i < lastName.Length; i++)
            {
                if (char.IsDigit(lastName[i]))
                {
                    lastNameTB.Background = Brushes.Red;
                    return false;
                }
            }

            lastNameTB.Background = Brushes.White;
            return true;
        }
    }
}
