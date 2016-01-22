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
        string name, text;
        double price;
        Image imageTMP;
        public itemInformation(/*string name, string text, double price, Image image*/)
        {
            InitializeComponent();
            this.name = name;
            this.text = text;

        }
    }
}
