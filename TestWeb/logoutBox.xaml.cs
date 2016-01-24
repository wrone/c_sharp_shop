using c_sharp_kursa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for logoutBox.xaml
    /// </summary>
    public partial class logoutBox : UserControl
    {
        MainWindow mw;
        DatabaseConnection dbConn;
        string login;

        public logoutBox(MainWindow mw, DatabaseConnection dbConn, string login)
        {
            this.mw = mw;
            this.dbConn = dbConn;
            this.login = login;
            InitializeComponent();
            userNameTextBox.Content = this.login;
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            mw.login_logout_StackPanel.Children.Clear();
            loginHeaderBox lHB = new loginHeaderBox(mw, dbConn);
            mw.login_logout_StackPanel.Children.Add(lHB);

            mw.cartInfoBox.Children.Clear();
            mw.loginTmp = 0;
            mw.hideUnhideAddButton(1);

            //lHB.cB.itemList.Clear();

            mw.addNewsButton.Visibility = Visibility.Hidden;
            mw.testButton2.Visibility = Visibility.Hidden;
            mw.testButton3.Visibility = Visibility.Hidden;

            mw.hideUnhideAdminButton(1);
            mw.startPageButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
        }

        private void testButton_Click(object sender, RoutedEventArgs e)
        {
            //mw.stackPanelMain.Children.Clear();
            //productAdd pA = new productAdd();
            //mw.stackPanelMain.Children.Add(pA);
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            Window profileWindow = new Window
            {
                Title = "Your profile",
                Content = new userProfile(mw, dbConn, login),
                Width = 300,
                Height = 300,
                ResizeMode = ResizeMode.NoResize,
                WindowStartupLocation = WindowStartupLocation.CenterScreen

            };
            profileWindow.ShowDialog();
        }
    }
}
