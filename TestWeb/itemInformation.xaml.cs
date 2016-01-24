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
    /// Interaction logic for itemInformation.xaml
    /// </summary>
    public partial class itemInformation : UserControl
    {
        List<ProductClass> productList;
        itemFrame iF;

        MainWindow mw;
        int index;
        public itemInformation(MainWindow mw, List<ProductClass> productList)
        {
            InitializeComponent();
            this.mw = mw;
            this.productList = productList;
        }

        public void changeInfo(int index)
        {
            for (int i = 0; i < productList.Count; i++)
            {
                if (index == productList[i].getId())
                {
                    nameBox.Text = productList[i].getName();
                    descriptionBox.Text = productList[i].getDescription();
                    priceBox.Text = Convert.ToString(productList[i].getPrice());
                    image.Source = productList[i].getImage();
                }
            }
        }

        public void setItemFram(itemFrame iF)
        {
            this.iF = iF;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            Items item = new Items(index - 1, 1);
            int test = 0;
            for(int i = 0; i < mw.cartBoxNew.itemList.Count; i++)
            {
                if(index - 1 == mw.cartBoxNew.itemList[i].getIndex())
                {
                    test = 1;
                }
            }
            if (test == 0)
            {
                mw.cartBoxNew.itemList.Add(item);
                mw.cartBoxNew.productList = mw.productList;
                mw.cartBoxNew.imageList = mw.imageList;
            }
            else
                MessageBox.Show("Product is already in the cart", "Add product to cart", MessageBoxButton.OK, MessageBoxImage.Exclamation);

            mw.cartBoxNew.cartInfoNumber.Content = Convert.ToString(mw.cartBoxNew.itemList.Count);
            mw.cartBoxNew.PrepareCart();
            mw.cartBoxNew.OrderToCache(mw.cartBoxNew.name);
        }
    }
}
