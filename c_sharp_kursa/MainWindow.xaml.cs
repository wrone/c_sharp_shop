﻿using System;
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
        DatabaseConnection dbConn;

        public MainWindow()
        {
            InitializeComponent();
            //ConnectToDB("<database_ip>", "<port>", "<table_name>", "<username>", "<password>");

            dbConn = new DatabaseConnection("127.0.0.1", "3306", "shop", "root", "root");

            //List<string> list = dbConn.ReadData("SELECT * FROM Users");
            //MessageBox.Show(list[1]);

            //UserRegister("da", "da", "da2", "da", "da", "da");
            //ProductRegister("da", "da", "2008-11-11", "2008-11-11", 2, 2, "da", "da", "da");
            //PaymentRegister("2008-11-11", "da", "da", "da", "2008-11-11", 12);
            //AddressRegister("da", "da", "da2", "da", "da", "da");
        }

        //public void ConnectToDB(string host, string port, string dbName, string user, string password)
        //{
        //    MyConnection = "Server=" + host + ";Port=" + port + ";Database=" + dbName + ";Uid=" + user + ";Pwd=" + password + ";";
        //    connection = new MySqlConnection(MyConnection);
        //    connection.Open();
        //}

        ////Test query, just to know how command execution is proceeded. Function should be changed when the DB diagramm is done. 
        //public void ExecuteQuery()
        //{
        //    try
        //    {
        //        cmd = connection.CreateCommand();
        //        cmd.CommandText = "INSERT INTO Valsts(Pilseta, Valsts) VALUES(@Pilseta, @Valsts)";
        //        cmd.Parameters.AddWithValue("@Pilseta", "Ventspils");
        //        cmd.Parameters.AddWithValue("@Valsts", "Latvija");
        //        cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public void LoadData()
        //{
        //    string text = "";
        //    cmd = connection.CreateCommand();
        //    cmd.CommandText = "SELECT * FROM Gramata";
        //    dataReader = cmd.ExecuteReader();

        //    while (dataReader.Read())
        //    {
        //        text += dataReader["<column_name>"];
        //    }

        //    dataReader.Close();
        //}
        public bool UserRegister(string name, string lastname, string login, string password, string email, string phone)
        {
            List<string> loginList = dbConn.ReadData("select Login from Users");

            if(!loginList.Contains(login))
            {
                dbConn.WriteData("INSERT INTO Users(Name, Lastname, Login, Password, Email, Phone, Role)"
                               + "VALUES('" + name + "', '" + lastname + "', '" + login + "', '" 
                               + password + "', '" + email + "', '" + phone + "', 'User')");

                return true;
            }
            return false;
        }
        public int ProductRegister(string name, string desc, string releaseDate, string endDate, int quantity, double price, string category, string manufacturer, string picture)
        {
            dbConn.WriteData("INSERT INTO Products(Name, Description, Release_date, End_date, Quantity, Price, Category, Manufacturer, Picture)"
                           + "VALUES('" + name + "', '" + desc + "', '" + releaseDate + "', '"
                           + endDate + "', " + quantity + ", " + price + ", '" + category + "', '"
                           + manufacturer + "', '" + picture + "')");

            return GetLastID("Products");
        }
        public int PaymentRegister(string date, string cardNumber, string holdersName, string holdersLastname, string expDate, double money)
        {
            dbConn.WriteData("INSERT INTO Payments(Date, CardNumber, CardHoldersName, CardHoldersLastname, ExpDate, Amount)"
                            + "VALUES('" + date + "', '" + cardNumber + "', '" + holdersName + "', '"
                            + holdersLastname + "', '" + expDate + "', " + money + ")");

            return GetLastID("Payments");
        }
        public int AddressRegister(string name, string lastname, string phone, string email, string address, string city)
        {
            dbConn.WriteData("INSERT INTO Addresses(Name, Lastname, Phone, Email, Address, City)"
                           + "VALUES('" + name + "', '" + lastname + "', '" + phone + "', '"
                           + email + "', '" + address + "', '" + city + "')");

            return GetLastID("Addresses");
        }
        // param_id_product INT, param_quant INT, param_id_user INT, param_id_addr INT, param_id_shipp_m INT, param_id_payment INT
        public void BuyProduct(int productID, int quant, int userID, int addrID, int shipMetID, int payID)
        {

        }



        public int GetLastID(string table)
        {
            int id = Convert.ToInt32(dbConn.ReadData("select max(ID) from " + table + ")")[0]);
            return 0;
        }

    }
}
