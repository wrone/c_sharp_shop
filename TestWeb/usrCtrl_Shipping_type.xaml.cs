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
    /// Interaction logic for usrCtrl_Shipping_type.xaml
    /// </summary>
    public partial class usrCtrl_Shipping_type : UserControl
    {
        public usrCtrl_Shipping_type()
        {
            InitializeComponent();

            comboBox.Items.Add("DPD");
            comboBox.Items.Add("...");
        }
    }
}
