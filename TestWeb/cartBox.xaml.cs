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
        MainWindow mw;
        public cartBox(MainWindow mw)
        {
            this.mw = mw;
            InitializeComponent();
        }
    }
}
