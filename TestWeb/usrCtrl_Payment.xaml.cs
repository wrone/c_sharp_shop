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
    /// Interaction logic for usrCtrl_Payment.xaml
    /// </summary>
    public partial class usrCtrl_Payment : UserControl
    {
        public usrCtrl_Payment()
        {
            InitializeComponent();

            for (int i = 0; i < 12; i++)
                comboBox.Items.Add((i + 1).ToString());

            DateTime dt = DateTime.Now;
            comboBox1.Items.Add(dt.Year.ToString());
            comboBox1.Items.Add((dt.Year + 1).ToString());
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            ((Panel)this.Parent).Children.Remove(this);
        }

        public Payment GetPayment()
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string date = dateTime.ToString("yyyy-MM-dd");
            string expDate = comboBox.SelectedValue + "/" + comboBox1.SelectedValue;
            return new Payment(-1, date, textBox.Text, textBox1.Text, textBox2.Text, expDate, 777);
        }


    }
}

