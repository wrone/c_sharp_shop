using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace c_sharp_kursa
{
    /// <summary>
    /// Interaction logic for productAdd.xaml
    /// </summary>
    public partial class productAdd : UserControl
    {
        private DatabaseConnection conn;
        private FileStream fs;

        public productAdd()
        {
            InitializeComponent();
            
        }

        public void SetConnection(DatabaseConnection conn)
        {
            this.conn = conn;
        }

        private void productPictureSelectButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            //dlg.DefaultExt = ".jpg";
            //dlg.Filter = "Text documents (.txt)|*.txt";

            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;

                fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                //BufferedStream bf = new BufferedStream(fs);
                //byte[] buffer = new byte[bf.Length];
                //bf.Read(buffer, 0, buffer.Length);
                //buffer_new = buffer;

                //string query = @"INSERT INTO Products(id, fileimage) VALUES ('stringhex',x'" + hex + "')";

            }

        }

        private void productAddButton_Click(object sender, RoutedEventArgs e)
        {
            //MySqlConnection connection = new MySqlConnection(MyConString);
            //connection.Open();
            //MySqlCommand command = new MySqlCommand("", connection);
            //command.CommandText = "insert into table(fldImage) values(@image);";

            //command.Parameters.AddWithValue("@image", buffer_new);

            //command.ExecuteNonQuery();

            //connection.Close();

            //Console.WriteLine("Task Performed!");
            //Console.ReadLine();

            //conn.WriteDataWithValue("insert into Products(Picture) values(@image)", "@image", buffer_new);

            byte[] rawData = new byte[fs.Length];
            fs.Read(rawData, 0, (int)fs.Length);
            fs.Close();
            //byte[] to HEX STRING
            string hex = BitConverter.ToString(rawData);
            //'F3-F5-01-A3' to 'F3F501A3'
            hex = hex.Replace("-", "");

            ProductRegister(txtBoxName.Text, txtBoxDesc.Text, txtBoxReleaseDate.Text, txtBoxEndDate.Text, 
                Convert.ToInt32(txtBoxQuantity.Text), Convert.ToInt32(txtBoxPrice.Text), 
                cmbBoxCategory.Text, txtBoxManufacturer.Text, hex);
        }

        private void ProductRegister(string name, string desc, string releaseDate, string endDate, int quantity, double price, string category, string manufacturer, string picture)
        {
            string query = @"INSERT INTO Products(Name, Description, Release_date, End_date, Quantity, Price, Category, Manufacturer, Picture)"
                           + "VALUES('" + name + "', '" + desc + "', '" + releaseDate + "', '"
                           + endDate + "', " + quantity + ", " + price + ", '" + "categorija" + "', '"
                           + manufacturer + "', x'" + picture + "')";

            conn.WriteData(query);
        }





    }
}
