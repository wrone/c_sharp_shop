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
        byte[] buffer_new;
        DatabaseConnection conn;

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

                FileStream fs = new FileStream(@filename, FileMode.Open);
                BufferedStream bf = new BufferedStream(fs);
                byte[] buffer = new byte[bf.Length];
                bf.Read(buffer, 0, buffer.Length);
                buffer_new = buffer;
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
            conn.WriteDataWithValue("insert into Products(Picture) values(@image)", "@image", buffer_new);

        }



    }
}
