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
using TestWeb;

namespace c_sharp_kursa
{
    /// <summary>
    /// Interaction logic for userAdd.xaml
    /// </summary>
    public partial class userAdd : UserControl
    {
        MainWindow mw;
        DatabaseConnection dbConn;

        public userAdd(MainWindow mw, DatabaseConnection dbConn)
        {
            this.mw = mw;
            this.dbConn = dbConn;
            InitializeComponent();
        }

        private void userRegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string name = userName.Text;
            string login = userLogin.Text;
            string email = userEmail.Text;
            string password1 = userPassword1.Password;
            string password2 = userPassword2.Password;
            string lastName = userLastname.Text;
            string phone = userPhone.Text;

            if (checkEmail(email) & checkLogin(login) & checkName(email) & checkPassword(password1, password2))
            {
                bool lastNameOk = true, phoneOk = true;


                if (!lastName.Equals(""))
                    if (!checkLastName(lastName))
                        lastNameOk = false;
                if (!phone.Equals(""))
                    if (!checkPhone(phone))
                        phoneOk = false;

                if (lastNameOk && phoneOk)
                    if (mw.UserRegister(name, lastName, login, password1, email, phone))
                    {
                        MessageBox.Show("User was successfully added");
                        Window parent = Window.GetWindow(this);
                        parent.Close();
                        
                    }
            }
            else
                MessageBox.Show("User was not added. Please re-check your data.");
        }

       public bool checkName(string name)
        {
            for(int i=0; i<name.Length; i++)
            {
                if (char.IsDigit(name[i]))
                {
                    userName.Background = Brushes.Red;
                    return false;
                }
            }

            if (name.Equals(""))
            {
                userName.Background = Brushes.Red;
                return false;
            }
            userName.Background = Brushes.White;
            return true;
        }

        public bool checkLogin(string login)
        {
            string query = "SELECT Login FROM Users WHERE Login='" + login + "'";
            if (dbConn.ReadData(query).Count > 0 || login.Equals(""))
            {
                userLogin.Background = Brushes.Red;
                return false;
            }

            userLogin.Background = Brushes.White;
            return true;
        }

        public bool checkEmail(string email)
        {
            string pattern = @"([a-z]+)([@])([a-z]+)([.])([a-z]{2,3})";
            Regex regex = new Regex(pattern);

            if (!regex.IsMatch(email))
            {
                userEmail.Background = Brushes.Red;
                return false;
            }
            if (email.Equals(""))
            {
                userEmail.Background = Brushes.Red;
                return false;
            }
            userEmail.Background = Brushes.White;
            return true;
        }

        public bool checkPassword(string password1, string password2)
        {
            if(!password1.Equals("") && !password2.Equals(""))
                if (password1.Equals(password2))
                {
                    userPassword1.Background = Brushes.White;
                    userPassword2.Background = Brushes.White;
                    return true;
                }

            if (password1.Equals(""))
                userPassword1.Background = Brushes.Red;

            if (password2.Equals(""))
                userPassword2.Background = Brushes.Red;

            return false;
        }

        public bool checkPhone(string phone)
        {
            for (int i = 0; i < phone.Length; i++)
            {
                if (char.IsDigit(phone[i]))
                {
                    userPhone.Background = Brushes.White;
                    return true;
                }
            }
            userPhone.Background = Brushes.Red;
            return false;
        }

        public bool checkLastName(string lastName)
        {
            for (int i = 0; i < lastName.Length; i++)
            {
                if (char.IsDigit(lastName[i]))
                {
                    userLastname.Background = Brushes.Red;
                    return false;
                }
            }

            userLastname.Background = Brushes.White;
            return true;
        }


    }
}
