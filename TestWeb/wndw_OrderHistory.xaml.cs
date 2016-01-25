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
using System.Windows.Shapes;

namespace TestWeb
{
    /// <summary>
    /// Interaction logic for wndw_OrderHistory.xaml
    /// </summary>
    public partial class wndw_OrderHistory : Window
    {
        private DatabaseConnection conn;
        private int userID;
        private List<string> list;

        public wndw_OrderHistory(DatabaseConnection conn, string username)
        {
            InitializeComponent();
            this.conn = conn;
            userID = GetUserID(username);
            dataGrid.IsReadOnly = true;

            list = conn.ReadData("SELECT Date, Orders.ID FROM Orders WHERE Orders.Users_ID = " + userID);
            int counter = 1;
            for (int i = 0; i < list.Count; i += 2)
            {
                comboBox.Items.Add(counter + ". " + list[i].Substring(0, list[i].IndexOf(" ")));
                counter++;
            }
        }

        private string GetItemsQuery(int id)
        {
            return "select " +
                   "Products.Name, " +
                   "Products.Category, " +
                   "Products.Price, " +
                   "Order_items.Quantity " +
                   "FROM Order_items " +
                   "JOIN Orders on Order_items.Orders_ID = Orders.ID " +
                   "JOIN Products on Products.ID = Order_items.Products_ID " +
                   "WHERE Orders.ID = " + id;
        }

        private int GetUserID(string username)
        {
            return Convert.ToInt32(conn.ReadData("select ID from Users where Login='" + username + "'")[0]);
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dataGrid.DataContext = conn.ReadDataTable(GetItemsQuery(comboBox.SelectedIndex + 1));
        }
    }
}
