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
using System.Windows.Controls.Primitives;

namespace TestWeb
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<StackPanel> stackPanelList = new List<StackPanel>();
        public List<newsHomePage> newsList = new List<newsHomePage>();
        //List<typeShopBox> typeList = new List<typeShopBox>();
        // VAJSA
        //private string MyConnection;
        //private MySqlConnection connection;
        //private MySqlCommand cmd;
        //private MySqlDataReader dataReader;
        DatabaseConnection dbConn;
        public List<ProductClass> productList = new List<ProductClass>();
        public List<BitmapImage> imageList;

        public List<ProductClass> searchProductList = new List<ProductClass>();

        List<int> countOfItemsOnPage;
        public List<itemFrame> itemFrameList = new List<itemFrame>();

        itemInformation iI;
        List<string> ProteinCount;
        List<string> CreatineCount;
        List<string> AminoAcidsCount;
        List<string> CarbohydratesCount;

        bool searched = true;

        string pageTitle;
        public int loginTmp = 0;
        int currentNumber;

        public newsHomePage nHP;
        public cartBox cartBoxNew;


        public MainWindow()
        {
            InitializeComponent();
            dbConn = new DatabaseConnection("46.109.120.29", "3306", "shop", "csharp", "FSzWUcCcm8fAsdJe");
            //dbConn = new DatabaseConnection("127.0.0.1", "3306", "shop", "root", "root");

            dbConn.ReadBlobData(1);
            GetCategoryCount("Protein");

            //UserRegister("da", "da", "da32ac", "da", "da", "da");
            //ProductRegister("da", "da", "2008-11-11", "2008-11-11", 2, 2, "da", "da", "da");
            //PaymentRegister("2008-11-11", "da", "da", "da", "2008-11-11", 12);
            //AddressRegister("da", "da", "da2", "da", "da", "da");


            ProteinCount = dbConn.ReadData("select ID from Products WHERE Category LIKE 'Protein'");
            CreatineCount = dbConn.ReadData("select ID from Products WHERE Category LIKE 'Creatine'");
            AminoAcidsCount = dbConn.ReadData("select ID from Products WHERE Category LIKE 'Amino acids'");
            CarbohydratesCount = dbConn.ReadData("select ID from Products WHERE Category LIKE 'Carbohydrates'");

            List<string> ID = dbConn.ReadData("select ID from Products");
            List<string> Name = dbConn.ReadData("select Name from Products");
            List<string> Description = dbConn.ReadData("select Description from Products");
            //List<string> Release_date = dbConn.ReadData("select Release_date from Products");
            //List<string> End_date = dbConn.ReadData("select End_date from Products");
            List<string> Quantity = dbConn.ReadData("select Quantity from Products");
            List<string> Price = dbConn.ReadData("select Price from Products");
            List<string> Category = dbConn.ReadData("select Category from Products");
            List<string> Manufacturer = dbConn.ReadData("select Manufacturer from Products");
            imageList = new List<BitmapImage>();
            for (int i = 0; i < ID.Count; i++)
            {
                imageList.Add(dbConn.ReadBlobData(i));
            }
            for (int i = 0; i < ID.Count; i++)
            {
                ProductClass pC = new ProductClass(Convert.ToInt32(ID[i]), Name[i], Description[i], "a", "b", Convert.ToInt32(Quantity[i]), Convert.ToDouble(Price[i]), Manufacturer[i], imageList[i], Category[i]);
                productList.Add(pC);
            }

            Console.WriteLine(productList.Count);

            iI = new itemInformation(this, productList);


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

            addNews("Today is bbbbbbbbbb", "bbbbbbbbbbbbbbbbbbbbbbbbbbbbb", "1.21.2016 14:33");
            addNews("Today is aaaaaaaaaa", "aaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "1.21.2016 15:33");
            addNews("Today is cccccccccc", "cccccccccccccccccccc", "1.21.2016 16:33");
            addNews("Today is dddddddddd", "dddddddddddddddddddd", "1.21.2016 17:33");
            addNews("Today is eeeeeeeeee", "eeeeeeeeeeeeeeeeeeee");
            for (int i = 0; i < newsList.Count; i++)
            {
                homePage.Children.Add(newsList[i]);
            }


            // Login Register
            loginHeaderBox lHB = new loginHeaderBox(this, dbConn);
            login_logout_StackPanel.Children.Add(lHB);
            

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



        public int GetLastID(string table)
        {
            int id = Convert.ToInt32(dbConn.ReadData("select max(ID) from " + table)[0]);
            return 0;
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

            //scrollViewer.Visibility = Visibility.Visible;
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
            pageTitle = "Protein";
            hideOrUnhideAll(0);
            currentPageLabel.Content = "1";
            countOfItemsOnPage = new List<int>();
            allPageLabel.Content = getPageCountFull(ProteinCount.Count);
            changePage(productList, pageTitle, Convert.ToInt32(currentPageLabel.Content) - 1);
            searched = false;
        }

        private void creatineButton_Click(object sender, RoutedEventArgs e)
        {
            pageTitle = "Creatine";
            hideOrUnhideAll(0);
            currentPageLabel.Content = "1";
            countOfItemsOnPage = new List<int>();
            allPageLabel.Content = getPageCountFull(CreatineCount.Count);
            changePage(productList, pageTitle, Convert.ToInt32(currentPageLabel.Content) - 1);
            searched = false;
        }

        private void aminoAcidsButton_Click(object sender, RoutedEventArgs e)
        {
            pageTitle = "Amino acids";
            hideOrUnhideAll(0);
            currentPageLabel.Content = "1";
            countOfItemsOnPage = new List<int>();
            allPageLabel.Content = getPageCountFull(AminoAcidsCount.Count);
            changePage(productList, pageTitle, Convert.ToInt32(currentPageLabel.Content) - 1);
            searched = false;
        }

        private void carbohydratesButton_Click(object sender, RoutedEventArgs e)
        {
            pageTitle = "Carbohydrates";
            hideOrUnhideAll(0);
            currentPageLabel.Content = "1";
            countOfItemsOnPage = new List<int>();
            allPageLabel.Content = getPageCountFull(CarbohydratesCount.Count);
            changePage(productList, pageTitle, Convert.ToInt32(currentPageLabel.Content) - 1);
            searched = false;
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

                scrollViewer.Visibility = Visibility.Hidden;
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

                scrollViewer.Visibility = Visibility.Visible;

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

        public void changePage(List<ProductClass> list, string category, int page)
        {
            VisibilityChanger(0, Visibility.Visible);

            for (int i = 0; i < 10; i++)
                stackPanelList[i].Children.Clear();

            int counter = 0;
            int productCounter = 0;
            int startFrom = page * 10;

            foreach (ProductClass pc in list)
            {
                if (pc.GetCategory().Equals(category) && counter < startFrom) counter++;
                if (pc.GetCategory().Equals(category) && counter >= startFrom)
                {
                    itemFrame iF = new itemFrame(this, iI);
                    iF.nameLabel.Content = pc.getName();
                    iF.priceLabel.Content = pc.getPrice();
                    iF.SetImage(dbConn.ReadBlobData(pc.getId()));
                    iF.setIndex(pc.getId());
                    stackPanelList[productCounter].Children.Add(iF);
                    itemFrameList.Add(iF);
                    productCounter++;

                    if (productCounter == 10) break;
                }
            }

            VisibilityChanger(productCounter, Visibility.Hidden);

            if (loginTmp == 0)
                hideUnhideAddButton(1);
        }

        public void VisibilityChanger(int idx, Visibility v)
        {
            for (int i = idx; i < stackPanelList.Count; i++)
                stackPanelList[i].Visibility = v;
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
            nHP = new newsHomePage();
            nHP.addNew(name,text,date);
            newsList.Add(nHP);
        }

        public void addNews(string name, string text)
        {
            nHP = new newsHomePage();
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
                if (searched == true) changePage(searchProductList, pageTitle, Convert.ToInt32(currentPageLabel.Content) - 1);
                else changePage(productList, pageTitle, Convert.ToInt32(currentPageLabel.Content) - 1);
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
                if(searched == true) changePage(searchProductList, pageTitle, Convert.ToInt32(currentPageLabel.Content) - 1);
                else changePage(productList, pageTitle, Convert.ToInt32(currentPageLabel.Content) - 1);
            }
        }

        private void Sort(int type)
        {
            for (int i = 0; i < productList.Count; i++)
                for (int j = i + 1; j < productList.Count; j++)
                    if (type == 1 && productList[i].getId() > productList[j].getId()) Swap(i, j);               //by date asc
                    else if (type == 2 && productList[i].getId() < productList[j].getId()) Swap(i, j);          //by date desc
                    else if (type == 3 && productList[i].getPrice() > productList[j].getPrice()) Swap(i, j);    //by price asc
                    else if (type == 4 && productList[i].getPrice() < productList[j].getPrice()) Swap(i, j);    //by price desc
        }

        private void Swap(int idx1, int idx2)
        {
            ProductClass tmp = productList[idx1];
            productList[idx1] = productList[idx2];
            productList[idx2] = tmp;
        }

        private void DetermineButton(string s)
        {
            if (s.Equals("Protein")) proteinButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            else if (s.Equals("Creatine")) creatineButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            else if (s.Equals("Amino acids")) aminoAcidsButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            else if (s.Equals("Carbohydrates")) carbohydratesButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
        }

        private void button_Click_1(object sender, RoutedEventArgs e)   //by date asc
        {
            Sort(1);
            DetermineButton(pageTitle);
        }

        private void button1_Click(object sender, RoutedEventArgs e)    //by date desc
        {
            Sort(2);
            DetermineButton(pageTitle);
        }

        private void button2_Click(object sender, RoutedEventArgs e)    //by price asc
        {
            Sort(3);
            DetermineButton(pageTitle);
        }

        private void button3_Click(object sender, RoutedEventArgs e)    //by price desc
        {
            Sort(4);
            DetermineButton(pageTitle);
        }

        private void button4_Click(object sender, RoutedEventArgs e)    //search
        {
            if (textBox.Text.Length > 0)
            {
                searchProductList = new List<ProductClass>();
                List<string> tmp = dbConn.ReadData("call productSearch('" + textBox.Text + "', '" + pageTitle + "')");
                foreach (string s in tmp)
                    foreach (ProductClass p in productList)
                        if (p.getId() == Convert.ToInt32(s))
                        {
                            searchProductList.Add(p);
                            Console.WriteLine(s + ",");
                            break;
                        }

                searched = true;
                changePage(searchProductList, pageTitle, Convert.ToInt32(currentPageLabel.Content) - 1);
            }
        }

        private void button5_Click(object sender, RoutedEventArgs e)    //by price asc
        {
            PopularityHelper("SELECT * FROM mostPopularASC");
            DetermineButton(pageTitle);
        }

        private void button6_Click(object sender, RoutedEventArgs e)    //by price desc
        {
            PopularityHelper("SELECT * FROM mostPopularDESC");
            DetermineButton(pageTitle);
        }

        private void PopularityHelper(string s)
        {
            List<string> tmp = dbConn.ReadData(s);
            for (int i = 0; i < tmp.Count; i++)
                for (int j = 0; j < productList.Count; j++)
                    if (productList[j].getId() == Convert.ToInt32(tmp[i]))
                    {
                        Swap(i, j);
                        break;
                    }
        }
        //////////////////////////////////////////////////////////////////////////////////////////


    }
}
