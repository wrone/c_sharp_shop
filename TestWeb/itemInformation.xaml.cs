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

            //index = iF.getIndex();
        }

        public void changeInfo(int index)
        {
            this.index = index;
            nameBox.Text = productList[index - 1].getName();
            descriptionBox.Text = productList[index - 1].getDescription();
            priceBox.Text = Convert.ToString(productList[index - 1].getPrice());
            image.Source = productList[index].getImage();
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

            mw.cartBoxNew.cartInfoNumber.Content = Convert.ToString(mw.cartBoxNew.itemList.Count);
        }
    }
}
