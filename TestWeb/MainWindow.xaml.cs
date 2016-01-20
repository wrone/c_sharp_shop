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
using MySql.Data.MySqlClient;
using System.Data;

namespace TestWeb
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<itemFrame> itemsList = new List<itemFrame>();
        List<StackPanel> stackPanelList = new List<StackPanel>();
        //List<typeShopBox> typeList = new List<typeShopBox>();

        //private string MyConnection;
        //private MySqlConnection connection;
        //private MySqlCommand cmd;
        //private MySqlDataReader dataReader;
        DatabaseConnection dbConn;

        public MainWindow()
        {
            InitializeComponent();
            dbConn = new DatabaseConnection("46.109.120.29", "3306", "shop", "csharp", "FSzWUcCcm8fAsdJe");

            //UserRegister("da", "da", "da32ac", "da", "da", "da");
            //ProductRegister("da", "da", "2008-11-11", "2008-11-11", 2, 2, "da", "da", "da");
            //PaymentRegister("2008-11-11", "da", "da", "da", "2008-11-11", 12);
            //AddressRegister("da", "da", "da2", "da", "da", "da");

            stackPanelList.Add(sp1);
            stackPanelList.Add(sp2);
            stackPanelList.Add(sp3);
            stackPanelList.Add(sp4);
            stackPanelList.Add(sp5);
            stackPanelList.Add(sp6);
            stackPanelList.Add(sp7);
            stackPanelList.Add(sp8);
            stackPanelList.Add(sp9);
            stackPanelList.Add(sp10);


            itemFrame iF;
            for (int i = 0; i < 10; i++)
            {
                iF = new itemFrame();
                
                stackPanelList[i].Children.Add(iF);
            }

            // Login Register
            loginHeaderBox lHB = new loginHeaderBox(this);
            login_logout_StackPanel.Children.Add(lHB);
            
            //typeShopBox tSB = new typeShopBox("Man");
            //typeList.Add(tSB);
            //tSB = new typeShopBox("WoMan");
            //typeList.Add(tSB);

            //for (int i = 0; i < typeList.Count; i++)
            //{
            //    typeMenu.Children.Add(typeList[i]);
            //}


            //itemFrame itF1 = new itemFrame();
            //itF1.nameLabel.Content = "Nam1e";
            //itF1.textLabel.Content = "Nam1e";
            //itF1.priceLabel.Content = "2.14";
            //grid.Children.Add(itF1);

            //itemFrame itF = new itemFrame();
            //itF.nameLabel.Content = "Nam2e";
            //itF.textLabel.Content = "Nam2e";
            //itF.priceLabel.Content = "3.14";
            //grid.Children.Add(itF);

            //userAdd ua = new userAdd();
            //grid.Children.Add(ua);

        }

        private void startPageButton_Click(object sender, RoutedEventArgs e)
        {
            //stackPanelList[3].Children[0].priceLabel.text
        }

        public bool UserRegister(string name, string lastname, string login, string password, string email, string phone)
        {
            List<string> loginList = dbConn.ReadData("select Login from Users");

            if (!loginList.Contains(login))
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
        public void BuyProduct(int productID, int quant, int userID, int addrID, int shipMetID, int payID)
        {

        }



        public int GetLastID(string table)
        {
            int id = Convert.ToInt32(dbConn.ReadData("select max(ID) from " + table)[0]);
            return 0;
        }

    }
}
