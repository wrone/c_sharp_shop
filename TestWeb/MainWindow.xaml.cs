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
using System.Data;
using System.Globalization;

namespace TestWeb
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<itemFrame> itemsList = new List<itemFrame>();
        List<StackPanel> stackPanelList = new List<StackPanel>();
        List<newsHomePage> newsList = new List<newsHomePage>();
        //List<typeShopBox> typeList = new List<typeShopBox>();
        // VAJSA
        //private string MyConnection;
        //private MySqlConnection connection;
        //private MySqlCommand cmd;
        //private MySqlDataReader dataReader;
        DatabaseConnection dbConn;
        List<ProductClass> productList = new List<ProductClass>();

        List<int> countOfItemsOnPage;
        List<itemFrame> itemFrameList = new List<itemFrame>();

        itemInformation iI;
        List<string> ProteinCount;
        List<string> CreatineCount;
        List<string> AminoAcidsCount;
        List<string> CarbohydratesCount;

        int catergoryType;
        public int loginTmp = 0;
        int currentNumber;


        public MainWindow()
        {
            InitializeComponent();
            //dbConn = new DatabaseConnection("46.109.120.29", "3306", "shop", "csharp", "FSzWUcCcm8fAsdJe");
            dbConn = new DatabaseConnection("127.0.0.1", "3306", "shop", "root", "root");

            int payID = PaymentRegister("2008-11-11", "asd", "asd", "asd", "2008-11-11", "14,87");
            int addrID =  AddressRegister("asd", "asd", "asd", "asd", "asd", "asd");
            // BuyProduct(int productID, int quantity, int userID, int addrID, int shipID, int payID)
            BuyProduct(1, 3, 1, 4, 1, 4);

            //dbConn.ReadBlobData(1);
            //GetCategoryCount("Protein");

            ////UserRegister("da", "da", "da32ac", "da", "da", "da");
            ////ProductRegister("da", "da", "2008-11-11", "2008-11-11", 2, 2, "da", "da", "da");
            ////PaymentRegister("2008-11-11", "da", "da", "da", "2008-11-11", 12);
            ////AddressRegister("da", "da", "da2", "da", "da", "da");


            //ProteinCount = dbConn.ReadData("select ID from Products WHERE Category LIKE 'Protein'");
            //CreatineCount = dbConn.ReadData("select ID from Products WHERE Category LIKE 'Creatine'");
            //AminoAcidsCount = dbConn.ReadData("select ID from Products WHERE Category LIKE 'AminoAcids'");
            //CarbohydratesCount = dbConn.ReadData("select ID from Products WHERE Category LIKE 'Carbohydrates'");

            //List<string> ID = dbConn.ReadData("select ID from Products");
            //List<string> Name = dbConn.ReadData("select Name from Products");
            //List<string> Description = dbConn.ReadData("select Description from Products");
            ////List<string> Release_date = dbConn.ReadData("select Release_date from Products");
            ////List<string> End_date = dbConn.ReadData("select End_date from Products");
            //List<string> Quantity = dbConn.ReadData("select Quantity from Products");
            //List<string> Price = dbConn.ReadData("select Price from Products");
            //List<string> Manufacturer = dbConn.ReadData("select Manufacturer from Products");
            //for (int i = 0; i < ID.Count; i++)
            //{
            //    ProductClass pC = new ProductClass(Convert.ToInt32(ID[i]), Name[i], Description[i], "a", "b", Convert.ToInt32(Quantity[i]), Convert.ToDouble(Price[i]), Manufacturer[i]);
            //    productList.Add(pC);
            //}

            //iI = new itemInformation(this, productList);


            //stackPanelList.Add(sp1);
            //stackPanelList.Add(sp2);
            //stackPanelList.Add(sp3);
            //stackPanelList.Add(sp4);
            //stackPanelList.Add(sp5);
            //stackPanelList.Add(sp6);
            //stackPanelList.Add(sp7);
            //stackPanelList.Add(sp8);
            //stackPanelList.Add(sp9);
            //stackPanelList.Add(sp10);

            //addNews("Today is bbbbbbbbbb", "bbbbbbbbbbbbbbbbbbbbbbbbbbbbb", "1.21.2016 14:33");
            //addNews("Today is aaaaaaaaaa", "aaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            //for (int i = 0; i < newsList.Count; i++)
            //{
            //    homePage.Children.Add(newsList[i]);
            //}


            //// Login Register
            //loginHeaderBox lHB = new loginHeaderBox(this, dbConn);
            //login_logout_StackPanel.Children.Add(lHB);


        }

        public void BuyProduct(int productID, int quantity, int userID, int addrID, int shipID, int payID)
        {
            dbConn.ReadData("select productBuy(" + productID + ", " + quantity + ", " + userID + ", " + addrID + ", " + shipID + ", " + payID + ")");
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
        public int ProductRegister(string name, string desc, string releaseDate, string endDate, int quantity, double price, string category, string manufacturer, Image image)
        {
            dbConn.WriteData("INSERT INTO Products(Name, Description, Release_date, End_date, Quantity, Price, Category, Manufacturer, Picture)"
                           + "VALUES('" + name + "', '" + desc + "', '" + releaseDate + "', '"
                           + endDate + "', " + quantity + ", " + price + ", '" + category + "', '"
                           + manufacturer + "', '" + image + "')");

            return GetLastID("Products");
        }
        public List<string> ProductReader()
        {
            return dbConn.ReadData("SELECT ID, Description, Release_date, End_date, Quantity, Price, Category, Manufacturer FROM Products");
        }

        public int PaymentRegister(string date, string cardNumber, string holdersName, string holdersLastname, string expDate, string money)
        {
            dbConn.WriteData("INSERT INTO Payments(Date, CardNumber, CardHoldersName, CardHoldersLastname, ExpDate, Amount)"
                            + " VALUES('" + date + "', '" + cardNumber + "', '" + holdersName + "', '" + holdersLastname + "', '" + expDate + "', '" + money.Replace(",", ".") + "')");

            return GetLastID("Payments");
        }
        public int AddressRegister(string name, string lastname, string phone, string email, string address, string city)
        {
            dbConn.WriteData("INSERT INTO Addresses(Name, Lastname, Phone, Email, Address, City)"
                           + "VALUES('" + name + "', '" + lastname + "', '" + phone + "', '"
                           + email + "', '" + address + "', '" + city + "')");

            return GetLastID("Addresses");
        }



        public int GetLastID(string table)
        {
            return Convert.ToInt32(dbConn.ReadData("select max(ID) from " + table)[0]);
        }





        public int GetCategoryCount(string category)
        {
            //Console.WriteLine(dbConn.ReadData("SELECT ID FROM Products WHERE Category = " + category));
            return 0;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            ProductRegister reg = new ProductRegister(dbConn);
            reg.Show();
        }
        //////////////////////////////////////////////////////////////////////////////////////////Buttons
        private void startPageButton_Click(object sender, RoutedEventArgs e)
        {
            homePage.Children.Clear();
            hideOrUnhideAll(1);
            for(int i = 0; i < newsList.Count; i++)
            {
                homePage.Children.Add(newsList[i]);
            }
        }

        private void contactButton_Click(object sender, RoutedEventArgs e)
        {
            homePage.Children.Clear();
            hideOrUnhideAll(1);
            contactInfo cI = new contactInfo();
            homePage.Children.Add(cI);
        }

        private void testButton_Click(object sender, RoutedEventArgs e) ////////////////////////////////////////////////////////////////test
        {

        }

        private void proteinButton_Click(object sender, RoutedEventArgs e)
        {
            catergoryType = 1;
            hideOrUnhideAll(0);
            currentPageLabel.Content = "1";
            countOfItemsOnPage = new List<int>();
            allPageLabel.Content = getPageCountFull(ProteinCount.Count);
            changePage(catergoryType);
        }

        private void creatineButton_Click(object sender, RoutedEventArgs e)
        {
            catergoryType = 2;
            hideOrUnhideAll(0);
            currentPageLabel.Content = "1";
            countOfItemsOnPage = new List<int>();
            allPageLabel.Content = getPageCountFull(CreatineCount.Count);
            changePage(catergoryType);
        }

        private void aminoAcidsButton_Click(object sender, RoutedEventArgs e)
        {
            catergoryType = 3;
            hideOrUnhideAll(0);
            currentPageLabel.Content = "1";
            countOfItemsOnPage = new List<int>();
            allPageLabel.Content = getPageCountFull(AminoAcidsCount.Count);
            changePage(catergoryType);
        }

        private void carbohydratesButton_Click(object sender, RoutedEventArgs e)
        {
            catergoryType = 4;
            hideOrUnhideAll(0);
            currentPageLabel.Content = "1";
            countOfItemsOnPage = new List<int>();
            allPageLabel.Content = getPageCountFull(CarbohydratesCount.Count);
            changePage(catergoryType);
        }
        //////////////////////////////////////////////////////////////////////////////////////////Hide home page and unhide 10stackpanel
        public void hideOrUnhideAll(int sk) // 0 unhide ... 1 hide
        {
            if (sk == 0)
            {
                for (int i = 0; i < stackPanelList.Count; i++)
                {
                    stackPanelList[i].Visibility = Visibility.Visible;
                }
                currentPageLabel.Visibility = Visibility.Visible;
                label.Visibility = Visibility.Visible;
                allPageLabel.Visibility = Visibility.Visible;

                PreviousButton.Visibility = Visibility.Visible;
                nextButton.Visibility = Visibility.Visible;

                homePage.Visibility = Visibility.Hidden;
            }
            else if (sk == 1)
            {
                for (int i = 0; i < stackPanelList.Count; i++)
                {
                    stackPanelList[i].Visibility = Visibility.Hidden;
                }
                currentPageLabel.Visibility = Visibility.Hidden;
                label.Visibility = Visibility.Hidden;
                allPageLabel.Visibility = Visibility.Hidden;

                PreviousButton.Visibility = Visibility.Hidden;
                nextButton.Visibility = Visibility.Hidden;

                homePage.Visibility = Visibility.Visible;
            }
        }

        public void hideUnhideAddButton(int sk) // 0 unhide ... 1 hide
        {
            if(sk == 0)
            {
                for(int i = 0; i < itemFrameList.Count; i++)
                {
                    itemFrameList[i].button.Visibility = Visibility.Visible;
                    iI.addButton.Visibility = Visibility.Visible;
                }
            }
            else if(sk == 1)
            {
                for (int i = 0; i < itemFrameList.Count; i++)
                {
                    itemFrameList[i].button.Visibility = Visibility.Hidden;
                    iI.addButton.Visibility = Visibility.Hidden;
                }
            }

        }
        //////////////////////////////////////////////////////////////////////////////////////////functions
        public void changePage(int type)
        {
            int number = 0;
            int test = 1;

            for (int i = 0; i < 10; i++)
                stackPanelList[i].Children.Clear();
            itemFrame iF;
            for (int i = 0; i < countOfItemsOnPage.Count; i++)
            {
                if (i + 1 == Convert.ToInt32(currentPageLabel.Content))
                {
                    for (int j = 0; j < countOfItemsOnPage[i]; j++)
                    {
                        if (type == 2 && test == 1)
                        {
                            number += ProteinCount.Count;
                            test = 0;
                        }
                        if (type == 3 && test == 1)
                        {
                            number += ProteinCount.Count + CreatineCount.Count;
                            test = 0;
                        }
                        if (type == 4 && test == 1)
                        {
                            number += ProteinCount.Count + CreatineCount.Count + AminoAcidsCount.Count;
                            test = 0;
                        }

                        iF = new itemFrame(this, iI);
                        iF.nameLabel.Content = productList[number + (i * 10) + j].getName();
                        iF.priceLabel.Content = productList[number + (i * 10) + j].getPrice();
                        iF.SetImage(dbConn.ReadBlobData(number + (i * 10) + j + 1));
                        iF.setIndex(number + (i * 10) + j + 1);
                        stackPanelList[j].Children.Add(iF);
                        itemFrameList.Add(iF);

                        if(loginTmp == 0)
                            hideUnhideAddButton(1);
                    }
                }

            }

            for (int i = 0; i < stackPanelList.Count; i++)
            {
                if (stackPanelList[i].Children.Count == 0)
                    stackPanelList[i].Visibility = Visibility.Hidden;
                else
                    stackPanelList[i].Visibility = Visibility.Visible;
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////functions
        public int getPageCountFull(int count)
        {
            int pages = 0, tmp = count;
            for (int i = 0; i < 10; i++)
            {
                if (tmp >= 10)
                {
                    countOfItemsOnPage.Add(10);
                    tmp -= 10;
                    pages++;
                }
                else if (tmp > 0 && tmp < 10)
                {
                    countOfItemsOnPage.Add(tmp);
                    tmp -= 10;
                    pages++;
                }
                else
                {
                    break;
                }
            }
            return pages;
        }

        public int getAllPages()
        {
            return Convert.ToInt32(allPageLabel.Content);
        }
        //////////////////////////////////////////////////////////////////////////////////////////News info functions
        public void addNews(string name, string text, string date)
        {
            newsHomePage nHP = new newsHomePage();
            nHP.addNew(name,text,date);
            newsList.Add(nHP);
        }

        public void addNews(string name, string text)
        {
            newsHomePage nHP = new newsHomePage();
            nHP.addNew(name, text);
            newsList.Add(nHP);
        }
        //////////////////////////////////////////////////////////////////////////////////////////Button < >
        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            int nm;
            if(currentPageLabel.Content != "1")
            {
                nm = Convert.ToInt32(allPageLabel.Content);
                nm = nm - 1;
                currentPageLabel.Content = nm;
                changePage(catergoryType);
            }
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            int nm;
            if (Convert.ToInt32(currentPageLabel.Content) != getAllPages())
            {
                nm = Convert.ToInt32(currentPageLabel.Content);
                nm = nm + 1;
                currentPageLabel.Content = nm;
                changePage(catergoryType);
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////


    }
}
