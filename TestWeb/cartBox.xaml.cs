using c_sharp_kursa;
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

namespace TestWeb
{
    /// <summary>
    /// Interaction logic for cartBox.xaml
    /// </summary>
    public partial class cartBox : UserControl
    {
        private DatabaseConnection conn;
        private MainWindow mw;
        public string name;
        public List<Items> itemList = new List<Items>();     
        public List<ProductClass> productList;            
        public List<BitmapImage> imageList;
        public List<usrCtrl_ItemInCart> itemListInCart;
        public usrCtrl_CartInfo cartUsr;
        public double price = 0;
        public string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public const string folderName = "MyProteinOrders";

        public cartBox()
        {
            InitializeComponent();
            mw = new MainWindow();
        }

        public cartBox(MainWindow mw, DatabaseConnection conn)
        {
            this.mw = mw;
            InitializeComponent();
            this.conn = conn;
            mw.cartBoxNew = this;

        }

        private void cartInfoButton_Click(object sender, RoutedEventArgs e)
        {
            price = 0;
            mw.homePage.Children.Clear();
            mw.hideOrUnhideAll(1);

            cartUsr = new usrCtrl_CartInfo(name, itemList, productList, conn, this);
            usrCtrl_ItemInCart item;
            itemListInCart = new List<usrCtrl_ItemInCart>();
            for (int i = 0; i < itemList.Count; i++)
            {
                item = new usrCtrl_ItemInCart(this, cartUsr);
                item.image.Source = imageList[itemList[i].getIndex()];
                item.text.Text = productList[itemList[i].getIndex()].getName();
                item.priceBox.Text = Convert.ToString(productList[itemList[i].getIndex()].getPrice());
                item.quantityLabel.Content = productList[itemList[i].getIndex()].getQuantity();
                item.countBox.Text = itemList[i].getCount().ToString();


                cartUsr.stackPanel.Children.Add(item);
                itemListInCart.Add(item);

                price += productList[itemList[i].getIndex()].getPrice() * Convert.ToInt32(item.countBox.Text);
                item.Tag = i;
                item.priceBox.Text = (productList[itemList[i].getIndex()].getPrice() * Convert.ToInt32(item.countBox.Text)).ToString();
            }

            cartUsr.username.Content = name;

            cartUsr.currentPriceBox.Text = Convert.ToString(price);

            mw.homePage.Children.Add(cartUsr);

            OrderToCache(name);

            mw.ReadData();
        }

        public void RemoveAllOrders()
        {
            cartUsr.stackPanel.Children.Clear();
            itemListInCart.Clear();
            itemList.Clear();
            price = 0;
            cartUsr.currentPriceBox.Text = "0";
            OrderToCache(name);
            mw.cartBoxNew.cartInfoNumber.Content = Convert.ToString(mw.cartBoxNew.itemList.Count);
            cartUsr.checkProductList();
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public void RecalculateEndPrice()
        {
            price = 0;
            foreach(usrCtrl_ItemInCart iic in itemListInCart)
            {
                price += Double.Parse(iic.priceBox.Text);
            }

            cartUsr.currentPriceBox.Text = price.ToString();
            OrderToCache(name);
        }

        //Help function to use in itemFrame class
        public void PrepareCart()
        {
            cartUsr = new usrCtrl_CartInfo(name, itemList, productList, conn, this);
            usrCtrl_ItemInCart item;
            itemListInCart = new List<usrCtrl_ItemInCart>();

            for (int i = 0; i < itemList.Count; i++)
            {
                item = new usrCtrl_ItemInCart(this, cartUsr);

                cartUsr.stackPanel.Children.Add(item);
                itemListInCart.Add(item);
            }
        }

        //Save orders to file
        public void OrderToCache(string login)
        {
            bool exists = System.IO.Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" + folderName);

            if (!exists)
                System.IO.Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" + folderName);

            using (StreamWriter outputFile = new StreamWriter(mydocpath + @"\MyProteinOrders\" + login + ".txt"))
            {
                string orderInfo = "";

                if (productList != null)
                {
                    if (itemList.Count > 0)
                        for (int i = 0; i < itemList.Count; i++)
                        {
                            orderInfo += productList[itemList[i].getIndex()].getId() + "/" + productList[itemList[i].getIndex()].getPrice() + "/" +
                                    itemListInCart[i].countBox.Text + "|";
                        }
                    outputFile.WriteLine(orderInfo);
                    outputFile.Close();
                }
            }
        }

        public void LoadOrders(string login)
        {
            string pathToFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" + folderName + @"\" + login + ".txt";
            bool exists = System.IO.File.Exists(pathToFile);

            if (exists)
            {
                string text = System.IO.File.ReadAllText(pathToFile);
                if (!text.Equals(""))
                {
                    string[] orderInfo = text.Split('|');
                    List<string> orderInfoDetails = new List<string>();

                    for (int i = 0; i < orderInfo.Length - 1; i++)
                    {
                        string[] tmpInfo = orderInfo[i].Split('/');
                        FillCartLists(int.Parse(tmpInfo[0]), int.Parse(tmpInfo[2]));
                    }
                }
            }
        }

        public void mwReadData()
        {
            mw.ReadData();
        }

        public void FillCartLists(int id, int count)
        {
            Items item = new Items(id - 1, count);
            mw.cartBoxNew.itemList.Add(item);
            mw.cartBoxNew.productList = mw.productList;
            mw.cartBoxNew.imageList = mw.imageList;
            mw.cartBoxNew.cartInfoNumber.Content = Convert.ToString(mw.cartBoxNew.itemList.Count);
        }
    }
}
