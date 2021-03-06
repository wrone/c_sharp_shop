﻿using c_sharp_kursa;
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
        
        DatabaseConnection dbConn;
        public List<ProductClass> productList = new List<ProductClass>();
        public List<BitmapImage> imageList;

        List<int> countOfItemsOnPage;
        public List<itemFrame> itemFrameList = new List<itemFrame>();

        public List<newsHomePage> newsList = new List<newsHomePage>();
        public List<newsClass> newsClassList = new List<newsClass>();
        public newsHomePage nHP;

        itemInformation iI;
        List<string> ProteinCount;
        List<string> CreatineCount;
        List<string> AminoAcidsCount;
        List<string> CarbohydratesCount;

        bool searched = true;
        public List<ProductClass> searchProductList = new List<ProductClass>();


        string pageTitle;
        public int loginTmp = 0;

        public cartBox cartBoxNew;


        public MainWindow()
        {
            InitializeComponent();
            dbConn = new DatabaseConnection("46.109.120.29", "3306", "shop", "csharp", "FSzWUcCcm8fAsdJe");

            drawNews();
            startPageButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            ReadData();

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

            // Login Register
            loginHeaderBox lHB = new loginHeaderBox(this, dbConn);
            login_logout_StackPanel.Children.Add(lHB);

            labelSort1.Visibility = Visibility.Hidden;
            labelSort2.Visibility = Visibility.Hidden;
            labelSort3.Visibility = Visibility.Hidden;
            labelSort4.Visibility = Visibility.Hidden;
            labelSort5.Visibility = Visibility.Hidden;
            button.Visibility = Visibility.Hidden;
            button1.Visibility = Visibility.Hidden;
            button2.Visibility = Visibility.Hidden;
            button3.Visibility = Visibility.Hidden;
            button4.Visibility = Visibility.Hidden;
            button5.Visibility = Visibility.Hidden;
            button6.Visibility = Visibility.Hidden;
            textBox.Visibility = Visibility.Hidden;
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

        public void drawNews()
        {
            homePage.Children.Clear();
            

            for(int i = 0; i < newsClassList.Count; i++)
            {
                nHP = new newsHomePage(this, dbConn);
                nHP.newsName.Content = newsClassList[i].getName();
                nHP.newsText.Text = newsClassList[i].getText();
                nHP.newsDateLabel.Content = newsClassList[i].getDate();
                nHP.Tag = i;
                newsList.Add(nHP);
                homePage.Children.Add(nHP);
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            ProductRegister reg = new ProductRegister(dbConn);
            reg.Show();
            ReadData();
        }
        //////////////////////////////////////////////////////////////////////////////////////////Buttons
        private void startPageButton_Click(object sender, RoutedEventArgs e)
        {
            homePage.Children.Clear();
            hideOrUnhideAll(1);

            newsClassList.Clear();
            List<string> ID_News = dbConn.ReadData("select ID from News");
            List<string> Title_News = dbConn.ReadData("select Title from News");
            List<string> Text_News = dbConn.ReadData("select Text from News");
            List<string> Date_News = dbConn.ReadData("select Date from News");

            for (int i = 0; i < ID_News.Count; i++)
            {
                newsClass nC = new newsClass(Convert.ToInt32(ID_News[i]), Title_News[i], Text_News[i], Date_News[i]);
                newsClassList.Add(nC);
            }

            drawNews();
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
            hideUnhideAdminButton(0);
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

                button.Visibility = Visibility.Visible;
                button1.Visibility = Visibility.Visible;
                button2.Visibility = Visibility.Visible;
                button3.Visibility = Visibility.Visible;
                button4.Visibility = Visibility.Visible;
                button5.Visibility = Visibility.Visible;
                button6.Visibility = Visibility.Visible;
                textBox.Visibility = Visibility.Visible;
                labelSort1.Visibility = Visibility.Visible;
                labelSort2.Visibility = Visibility.Visible;
                labelSort3.Visibility = Visibility.Visible;
                labelSort4.Visibility = Visibility.Visible;
                labelSort5.Visibility = Visibility.Visible;

                textBox.Text = "";

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
                button.Visibility = Visibility.Hidden;
                button1.Visibility = Visibility.Hidden;
                button2.Visibility = Visibility.Hidden;
                button3.Visibility = Visibility.Hidden;
                button4.Visibility = Visibility.Hidden;
                button5.Visibility = Visibility.Hidden;
                button6.Visibility = Visibility.Hidden;
                textBox.Visibility = Visibility.Hidden;
                labelSort1.Visibility = Visibility.Hidden;
                labelSort2.Visibility = Visibility.Hidden;
                labelSort3.Visibility = Visibility.Hidden;
                labelSort4.Visibility = Visibility.Hidden;
                labelSort5.Visibility = Visibility.Hidden;


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

        public void hideUnhideAdminButton(int sk)
        {
            if (sk == 0)
            {
                for (int i = 0; i < newsList.Count; i++)
                {
                    newsList[i].editButton.Visibility = Visibility.Visible;
                    newsList[i].deleteButton.Visibility = Visibility.Visible;
                }
            }
            else if (sk == 1)
            {
                for (int i = 0; i < newsList.Count; i++)
                {
                    newsList[i].editButton.Visibility = Visibility.Hidden;
                    newsList[i].deleteButton.Visibility = Visibility.Hidden;
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
                    iF.SetImage(pc.getImage());
                    iF.setIndex(pc.getId());
                    stackPanelList[productCounter].Children.Add(iF);
                    itemFrameList.Add(iF);
                    productCounter++;

                    if (pc.getQuantity() == 0)
                    {
                        iF.button.IsEnabled = false;
                        iI.addButton.IsEnabled = false;
                    }
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

        //////////////////////////////////////////////////////////////////////////////////////////Button < >
        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            int nm;
            if (currentPageLabel.Content != "1")
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
                if (searched == true) changePage(searchProductList, pageTitle, Convert.ToInt32(currentPageLabel.Content) - 1);
                else changePage(productList, pageTitle, Convert.ToInt32(currentPageLabel.Content) - 1);
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////
        private void addNewsButton_Click(object sender, RoutedEventArgs e)
        {
            homePage.Children.Clear();
            hideOrUnhideAll(1);

            usrCtrl_addNews aN = new usrCtrl_addNews(this, dbConn);
            aN.addEditButton.Content = "Add";

            homePage.Children.Add(aN);
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

        public void ReadData()
        {
            productList = new List<ProductClass>();
            imageList = new List<BitmapImage>();
            ProteinCount = new List<string>();
            CreatineCount = new List<string>();
            AminoAcidsCount = new List<string>();
            CarbohydratesCount = new List<string>();

            List<string> dataList = dbConn.ReadData("SELECT ID, Name, Description, Release_date, End_date, Quantity, Price, Manufacturer, Category FROM Products");
            for (int i = 0; i < dataList.Count; i += 9)
            {
                BitmapImage img = dbConn.ReadBlobData(Convert.ToInt32(dataList[i]));
                imageList.Add(img);

                ProductClass pC = new ProductClass(Convert.ToInt32(dataList[i]),
                                                    dataList[i + 1],
                                                    dataList[i + 2],
                                                    dataList[i + 3],
                                                    dataList[i + 4], 
                                                    Convert.ToInt32(dataList[i + 5]), 
                                                    Convert.ToDouble(dataList[i + 6]),
                                                    dataList[i + 7],
                                                    img,
                                                    dataList[i + 8]
                                                  );
                productList.Add(pC);
            }

            foreach (ProductClass pc in productList)
                if ("Protein".Equals(pc.GetCategory())) ProteinCount.Add(pc.getId().ToString());
                else if ("Creatine".Equals(pc.GetCategory())) CreatineCount.Add(pc.getId().ToString());
                else if ("Amino acids".Equals(pc.GetCategory())) AminoAcidsCount.Add(pc.getId().ToString());
                else if ("Carbohydrates".Equals(pc.GetCategory())) CarbohydratesCount.Add(pc.getId().ToString());

        }

    }
}
