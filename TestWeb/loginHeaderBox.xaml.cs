﻿using c_sharp_kursa;
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

namespace TestWeb
{
    /// <summary>
    /// Interaction logic for loginHeaderBox.xaml
    /// </summary>
    public partial class loginHeaderBox : UserControl
    {
        MainWindow mw;
        DatabaseConnection dbConn;
        public cartBox cB;
        public loginHeaderBox(MainWindow mw, DatabaseConnection dbConn)
        {
            this.mw = mw;
            this.dbConn = dbConn;
            InitializeComponent();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = loginIn.Text;
            string password = passwordIn.Password;

            // Logout
            if (checkCredentials(login, password))
            {
                cB = new cartBox(mw, dbConn);
                mw.cartInfoBox.Children.Clear();
                cB.cartInfoNumber.Content = "0";
                cB.setName(login);
                mw.cartInfoBox.Children.Add(cB);

                logoutBox lB = new logoutBox(mw, dbConn, login);
                lB.userNameTextBox.Content = loginIn.Text;
                mw.login_logout_StackPanel.Children.Clear();
                mw.login_logout_StackPanel.Children.Add(lB);

                mw.loginTmp = 1;
                mw.hideUnhideAddButton(0);

                if (GetRole(login).Equals("Seller"))
                    mw.addNewProductButton.Visibility = Visibility.Visible;

                if (GetRole(login).Equals("Admin"))
                {
                    mw.addNewsButton.Visibility = Visibility.Visible;
                    mw.testButton2.Visibility = Visibility.Visible;
                    mw.testButton3.Visibility = Visibility.Visible;

                    mw.hideUnhideAdminButton(0);
                }
                mw.cartBoxNew.LoadOrders(login);
            }
            else
                MessageBox.Show("Login or password is invalid", "Invalid credentials", MessageBoxButton.OK, MessageBoxImage.Error);

        }

        private void signInButton_Click(object sender, RoutedEventArgs e)
        {
            Window registerWindow = new Window
            {
                Title = "New account",
                Content = new userAdd(mw, dbConn),
                Width = 300,
                Height = 320,
                ResizeMode = ResizeMode.NoResize
            };
            registerWindow.ShowDialog();

        }

        public bool checkCredentials(string login, string password)
        {
            string query = "SELECT Login, Password FROM Users WHERE Login='" + login + "' AND Password='" + password + "'";
            if (dbConn.ReadData(query).Count == 2)
                return true;
            return false;
        }

        public string GetRole(string login)
        {
            string query = "SELECT Role FROM Users WHERE Login='" + login + "'";
            return dbConn.ReadData(query)[0];
        }

    }
}
