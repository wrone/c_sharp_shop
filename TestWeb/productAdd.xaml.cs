using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace c_sharp_kursa
{
    /// <summary>
    /// Interaction logic for productAdd.xaml
    /// </summary>
    public partial class productAdd : UserControl
    {
        private DatabaseConnection conn;
        private FileStream fs;
        private Nullable<bool> result;
        private OpenFileDialog dlg;

        public productAdd()
        {
            InitializeComponent();
            AddCategories(cmbBoxCategory);
        }

        public void SetConnection(DatabaseConnection conn)
        {
            this.conn = conn;
        }

        private void productPictureSelectButton_Click(object sender, RoutedEventArgs e)
        {
            dlg = new OpenFileDialog();
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg";

            result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;

                fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            }

        }

        private void productAddButton_Click(object sender, RoutedEventArgs rea)
        {
            bool a = InputValidator(txtBoxName, 3);
            bool b = InputValidator(txtBoxDesc, 5);
            bool c = checkPrice(txtBoxPrice);
            bool d = InputValidator(txtBoxQuantity, 1);
            bool e = InputValidator(txtBoxManufacturer, 2);
            bool f = checkDate(datePicker1, datePicker2);

            if (!a && !b && c && !d && !e
                && cmbBoxCategory.SelectedIndex != -1
                && result == true
                && datePicker1.SelectedDate != null
                && datePicker2.SelectedDate != null
                && f)
            {
                openDialog.Background = Brushes.White;

                string releaseDate = datePicker1.SelectedDate.Value.Year + "-"
                                + DateHalper(datePicker1.SelectedDate.Value.Month) + "-"
                                + DateHalper(datePicker1.SelectedDate.Value.Day);

                string endDate = datePicker2.SelectedDate.Value.Year + "-"
                        + DateHalper(datePicker2.SelectedDate.Value.Month) + "-"
                        + DateHalper(datePicker2.SelectedDate.Value.Day);

                string hex = ImageToHEX(fs);

                ProductRegister(txtBoxName.Text, txtBoxDesc.Text, releaseDate, endDate,
                    Convert.ToInt32(txtBoxQuantity.Text), Convert.ToInt32(txtBoxPrice.Text),
                    cmbBoxCategory.Text, txtBoxManufacturer.Text, hex);
                MessageBox.Show("Product was successfully added!", "Add product", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (cmbBoxCategory.SelectedIndex == -1) cmbBoxCategory.IsDropDownOpen = true;
            else if (datePicker1.SelectedDate == null) datePicker1.IsDropDownOpen = true;
            else if (datePicker2.SelectedDate == null) datePicker2.IsDropDownOpen = true;
            else if (result != true) openDialog.Background = Brushes.Red;
        }

        public string ImageToHEX(FileStream fs)
        {
            byte[] rawData = new byte[fs.Length];
            fs.Read(rawData, 0, (int)fs.Length);
            fs.Close();
            //byte[] to HEX STRING
            string hex = BitConverter.ToString(rawData);
            //'F3-F5-01-A3' to 'F3F501A3'
            hex = hex.Replace("-", "");

            return hex;
        }

        private string DateHalper(int number)
        {
            string s = number < 10 ? "0" + number : number + "";
            return s;
        }

        private void ProductRegister(string name, string desc, string releaseDate, string endDate, int quantity, double price, string category, string manufacturer, string picture)
        {
            string query = @"INSERT INTO Products(Name, Description, Release_date, End_date, Quantity, Price, Category, Manufacturer, Picture)"
                           + "VALUES('" + name + "', '" + desc + "', '" + releaseDate + "', '"
                           + endDate + "', " + quantity + ", " + price + ", '" + "categorija" + "', '"
                           + manufacturer + "', x'" + picture + "')";

            conn.WriteData(query);
        }

        private void txtBoxPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void txtBoxQuantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void AddCategories(ComboBox cb)
        {
            cb.Items.Add("Protein");
            cb.Items.Add("Creatine");
            cb.Items.Add("Amino acids");
            cb.Items.Add("Carbohydrates");
            cb.Items.Add("Healthy Ingredients");
            cb.Items.Add("Blends & Formulas");
            cb.Items.Add("Tablets & Capsules");
            cb.Items.Add("Bars");
            cb.Items.Add("Drinks");
            cb.Items.Add("Foods");
        }

        public bool checkPrice(TextBox tb)
        {
            string pattern = @"^[0-9]{1,3}([.,][0-9]{1,2})?$";
            Regex regex = new Regex(pattern);

            if (!regex.IsMatch(tb.Text))
            {
                tb.Background = Brushes.Red;
                return false;
            }
            if (tb.Text.Equals(""))
            {
                tb.Background = Brushes.Red;
                return false;
            }
            tb.Background = Brushes.White;
            return true;
        }

        public bool checkDate(DatePicker dp1, DatePicker dp2)
        {
            int result = 6;

            if (dp1.SelectedDate == null || dp2.SelectedDate == null) return false;
            else result = DateTime.Compare(dp1.SelectedDate.Value, dp2.SelectedDate.Value);

            if (result < 0)                       // pervaja menjwe vtoroj
            {
                dp1.Background = Brushes.White;
                dp2.Background = Brushes.White;
                return true;                      // vse good
            }
            else if (result == 0)                 // odinakovie
            {
                dp1.Background = Brushes.Red;
                dp2.Background = Brushes.Red;
                return false;
            }
            else                                  // pervaja pozze vtoroj
            {
                dp1.Background = Brushes.Red;
                dp2.Background = Brushes.Red;
                return false;
            }
        }

    }
}
