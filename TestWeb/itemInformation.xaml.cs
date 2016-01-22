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
        int index;

        MainWindow mw;
        public itemInformation(MainWindow mw, List<ProductClass> productList)
        {
            InitializeComponent();
            this.mw = mw;
            this.productList = productList;

            nameLabel.Content = productList[index + 1].getName();
            descriptionLabel.Content = productList[index + 1].getDescription();
        }

        public void setIndex(int index)
        {
            this.index = index;
        }
    }
}
