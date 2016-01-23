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

            textBox.MaxLength = 12;
            textBox1.MaxLength = 45;
            textBox2.MaxLength = 45;
        }

        //private void button_Click(object sender, RoutedEventArgs e)
        //{
            //bool a = InputValidator(textBox, 12);
            //bool b = InputValidator(textBox1, 5);
            //bool c = InputValidator(textBox2, 5);

            //if (comboBox.SelectedIndex == -1) comboBox.IsDropDownOpen = true;
            //else if (comboBox1.SelectedIndex == -1) comboBox1.IsDropDownOpen = true;

            //if (!a && !b && !c && comboBox.SelectedIndex != -1 && comboBox1.SelectedIndex != -1) ((Panel)this.Parent).Children.Remove(this);
        //}

        public bool Execute()
        {
            bool a = InputValidator(textBox, 12);
            bool b = InputValidator(textBox1, 5);
            bool c = InputValidator(textBox2, 5);

            if (comboBox.SelectedIndex == -1) comboBox.IsDropDownOpen = true;
            else if (comboBox1.SelectedIndex == -1) comboBox1.IsDropDownOpen = true;

            if (!a && !b && !c && comboBox.SelectedIndex != -1 && comboBox1.SelectedIndex != -1) return true; //((Panel)this.Parent).Children.Remove(this);
            else return false;
        }

        public Payment GetPayment()
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string date = dateTime.ToString("yyyy-MM-dd");
            string expDate = comboBox1.SelectedValue + "-" + comboBox.SelectedValue + "-01";//comboBox.SelectedValue + "/" + comboBox1.SelectedValue;
            return new Payment(-1, date, textBox.Text, textBox1.Text, textBox2.Text, expDate, 777);
        }

        private void textBox2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsLetter(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void textBox1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsLetter(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private bool InputValidator(TextBox tb, int length)
        {
            bool failed = false;

            if (string.IsNullOrWhiteSpace(tb.Text) || tb.Text.Length < length)
            {
                tb.Background = Brushes.Red;
                failed = true;
            }
            else tb.Background = Brushes.White;

            return failed;
        }

    }
}

