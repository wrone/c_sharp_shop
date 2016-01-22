using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// Interaction logic for itemFrame.xaml
    /// </summary>
    public partial class itemFrame : UserControl
    {
        MainWindow mw;
        itemInformation iI;
        int index;
        public itemFrame(MainWindow mw, itemInformation iI)
        {
            this.mw = mw;
            this.iI = iI;
            InitializeComponent();
        }

        public void setIndex(int index)
        {
            this.index = index;
        }

        public void SetImage(BitmapImage img)
        {
            image.Source = img;
        }

        private void informationButton_Click(object sender, RoutedEventArgs e)
        {
            mw.homePage.Children.Clear();
            mw.hideOrUnhideAll(1);
            mw.homePage.Children.Add(iI);
        }
    }
}
