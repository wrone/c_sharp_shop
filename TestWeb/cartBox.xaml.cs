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

namespace TestWeb
{
    /// <summary>
    /// Interaction logic for cartBox.xaml
    /// </summary>
    public partial class cartBox : UserControl
    {
        private DatabaseConnection conn;
        private MainWindow mw;
        private string name;
        public List<Items> itemList = new List<Items>();     
        public List<ProductClass> productList;            
        public List<BitmapImage> imageList;
        public List<usrCtrl_ItemInCart> itemListInCart;
        public usrCtrl_CartInfo cartUsr;
        public double price = 0;

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

            cartUsr = new usrCtrl_CartInfo(name, itemList, productList, conn);
            usrCtrl_ItemInCart item;
            itemListInCart = new List<usrCtrl_ItemInCart>();
            for (int i = 0; i < itemList.Count; i++)
            {
                item = new usrCtrl_ItemInCart(this, cartUsr);
                item.image.Source = imageList[itemList[i].getIndex() + 1];
                item.text.Text = productList[itemList[i].getIndex()].getName();
                item.priceBox.Text = Convert.ToString(productList[itemList[i].getIndex()].getPrice());


                cartUsr.stackPanel.Children.Add(item);
                itemListInCart.Add(item);

                price += productList[itemList[i].getIndex()].getPrice() * Convert.ToInt32(item.countBox.Text);
                item.Tag = i;
            }

            cartUsr.username.Content = name;

            //for(int i = 0; i < itemList.Count; i++)
            //{
            //    price += itemListInCart[i].price; ///////////////////////////////////////////////////////////////////////////////////////////// / sdelat summu price zatem sdelat remove knopku
            //}

            cartUsr.currentPriceBox.Text = Convert.ToString(price);

            mw.homePage.Children.Add(cartUsr);
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
            
        }
    }
}
