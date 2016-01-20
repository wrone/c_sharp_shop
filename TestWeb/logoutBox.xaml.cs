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
    /// Interaction logic for logoutBox.xaml
    /// </summary>
    public partial class logoutBox : UserControl
    {
        MainWindow mw;
        public logoutBox(MainWindow mw)
        {
            this.mw = mw;
            InitializeComponent();
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            mw.login_logout_StackPanel.Children.Clear();
            loginHeaderBox lHB = new loginHeaderBox(mw);
            mw.login_logout_StackPanel.Children.Add(lHB);

            mw.cartInfoBox.Children.Clear();
        }

        private void testButton_Click(object sender, RoutedEventArgs e)
        {
            //mw.stackPanelMain.Children.Clear();
            //productAdd pA = new productAdd();
            //mw.stackPanelMain.Children.Add(pA);
        }
    }
}
