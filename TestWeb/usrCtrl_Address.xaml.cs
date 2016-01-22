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
    /// Interaction logic for usrCtrl_Address.xaml
    /// </summary>
    public partial class usrCtrl_Address : UserControl
    {
        public usrCtrl_Address()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            ((Panel)this.Parent).Children.Remove(this);
        }

        public Address GetAddress()
        {
            return new Address(-1, textBox.Text, textBox1.Text, textBox2.Text, textBox3.Text,
                textBox4.Text, textBox5.Text);
        }

    }
}
