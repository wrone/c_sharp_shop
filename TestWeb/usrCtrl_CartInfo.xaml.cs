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
    /// Interaction logic for usrCtrl_CartInfo.xaml
    /// </summary>
    public partial class usrCtrl_CartInfo : UserControl
    {
        string usernameTmp;
        List<Items> itemList;
        List<ProductClass> productList;
        DatabaseConnection conn;


        public usrCtrl_CartInfo(string name, List<Items> itemList, List<ProductClass> productList, DatabaseConnection conn)
        {
            InitializeComponent();
            usernameTmp = name;
            this.itemList = itemList;
            this.productList = productList;
            this.conn = conn;
        }

        private void orderButton_Click(object sender, RoutedEventArgs e)
        {
            wndw_Product_Buy window = new wndw_Product_Buy(conn, usernameTmp, itemList, currentPriceBox.Text);
            window.ShowDialog();
        }

        public List<Items> GetItemList()
        {
            return itemList;
        }

        public string GetUserName()
        {
            return usernameTmp;
        }

    }
}
