using c_sharp_kursa;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for usrCtrl_Shipping_type.xaml
    /// </summary>
    public partial class usrCtrl_Shipping_type : UserControl
    {
        DatabaseConnection conn;
        List<Shipment> shipmnetsList;

        public usrCtrl_Shipping_type()
        {
            InitializeComponent();
        }

        public void Init(DatabaseConnection conn)
        {
            this.conn = conn;
            shipmnetsList = new List<Shipment>();

            List<string> list = conn.ReadData("select ID, Name, Price from Shipping_methods");
            for (int i = 0; i < list.Count; i += 3)
            {
                decimal result = decimal.Parse(list[i + 2], CultureInfo.InvariantCulture);
                Shipment s = new Shipment(Convert.ToInt32(list[i]), list[i + 1], result);
                shipmnetsList.Add(s);
            }
            foreach (Shipment s in shipmnetsList)
                comboBox.Items.Add(s.Name);
        }

        //private void button_Click(object sender, RoutedEventArgs e)
        //{
        //    if (comboBox.SelectedIndex == -1) comboBox.IsDropDownOpen = true;
        //    else ((Panel)this.Parent).Children.Remove(this);
        //}

        public bool Execute()
        {
            if (comboBox.SelectedIndex == -1)
            {
                comboBox.IsDropDownOpen = true;
                return false;
            }
            else return true;
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            textBox.Text = shipmnetsList[comboBox.SelectedIndex].Price + "";
        }

        public Shipment GetShipment()
        {
            return shipmnetsList[comboBox.SelectedIndex];
        }


    }
}
