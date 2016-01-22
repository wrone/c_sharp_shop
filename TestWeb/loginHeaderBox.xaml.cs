using c_sharp_kursa;
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
        public loginHeaderBox(MainWindow mw)
        {
            this.mw = mw;
            InitializeComponent();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            // Logout
            logoutBox lB = new logoutBox(mw);
            lB.userNameTextBox.Content = loginIn.Text;
            mw.login_logout_StackPanel.Children.Clear();
            mw.login_logout_StackPanel.Children.Add(lB);

            cartBox cB = new cartBox(mw);
            mw.cartInfoBox.Children.Clear();
            mw.cartInfoBox.Children.Add(cB);

            mw.loginTmp = 1;
            mw.hideUnhideAddButton(0);

        }

        private void signInButton_Click(object sender, RoutedEventArgs e)
        {
            //mw.stackPanelMain.Children.Clear();
            //userAdd uA = new userAdd();
            //mw.stackPanelMain.Children.Add(uA);

        }
    }
}
