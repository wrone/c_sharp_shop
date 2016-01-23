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
    /// Interaction logic for usrCtrl_Address.xaml
    /// </summary>
    public partial class usrCtrl_Address : UserControl
    {
        public usrCtrl_Address()
        {
            InitializeComponent();
            textBox.MaxLength = 45;
            textBox1.MaxLength = 45;
            textBox2.MaxLength = 8;
            textBox4.MaxLength = 45;
            textBox4.MaxLength = 45;
            textBox5.MaxLength = 45;
        }

        //private void button_Click(object sender, RoutedEventArgs rea)
        //{
            //bool a = InputValidator(textBox, 5);
            //bool b = InputValidator(textBox1, 5);
            //bool c = InputValidator(textBox2, 8);
            //bool d = InputValidator(textBox3, 8);
            //bool e = InputValidator(textBox4, 8);
            //bool f = InputValidator(textBox5, 5);

            //if (!a && !b && !c && !d && !e && !f) ((Panel)this.Parent).Children.Remove(this);
        //}

        public bool Execute()
        {
            bool a = InputValidator(textBox, 5);
            bool b = InputValidator(textBox1, 5);
            bool c = InputValidator(textBox2, 8);
            bool d = checkEmail(textBox3, textBox3.Text);
            bool e = InputValidator(textBox4, 8);
            bool f = InputValidator(textBox5, 5);

            if (!a && !b && !c && !d && !e && !f) return true; //((Panel)this.Parent).Children.Remove(this);
            else return false;
        }


        public Address GetAddress()
        {
            return new Address(-1, textBox.Text, textBox1.Text, textBox2.Text, textBox3.Text,
                textBox4.Text, textBox5.Text);
        }

        private void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsLetter(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void textBox1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsLetter(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void textBox5_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsLetter(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void textBox2_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        public bool checkEmail(TextBox tb, string email)
        {
            string pattern = @"([a-z]+)([@])([a-z]+)([.])([a-z]{2,3})";
            Regex regex = new Regex(pattern);

            if (!regex.IsMatch(email))
            {
                tb.Background = Brushes.Red;
                return false;
            }
            if (email.Equals(""))
            {
                tb.Background = Brushes.Red;
                return false;
            }
            tb.Background = Brushes.White;
            return true;
        }


    }
}
