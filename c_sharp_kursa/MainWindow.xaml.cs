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
using MySql.Data.MySqlClient;
using System.Data;

namespace c_sharp_kursa
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string MyConnection;
        private MySqlConnection connection;
        private MySqlCommand cmd;
        private MySqlDataReader dataReader;

        public MainWindow()
        {
            InitializeComponent();
            ConnectToDB("<database_ip>", "<port>", "<table_name>", "<username>", "<password>");
        }

        public void ConnectToDB(string host, string port, string dbName, string user, string password)
        {
            MyConnection = "Server=" + host + ";Port=" + port + ";Database=" + dbName + ";Uid=" + user + ";Pwd=" + password + ";";
            connection = new MySqlConnection(MyConnection);
            connection.Open();
        }

        //Test query, just to know how command execution is proceeded. Function should be changed when the DB diagramm is done. 
        public void ExecuteQuery()
        {
            try
            {
                cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO Valsts(Pilseta, Valsts) VALUES(@Pilseta, @Valsts)";
                cmd.Parameters.AddWithValue("@Pilseta", "Ventspils");
                cmd.Parameters.AddWithValue("@Valsts", "Latvija");
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void LoadData()
        {
            string text = "";
            cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM Gramata";
            dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                text += dataReader["<column_name>"];
            }

            dataReader.Close();
        }

    }
}
