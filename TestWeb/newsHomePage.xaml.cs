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
    /// Interaction logic for newsHomePage.xaml
    /// </summary>
    public partial class newsHomePage : UserControl
    {
        public newsHomePage()
        {
            InitializeComponent();
        }

        public void addNew(string name, string text, string date)
        {
            newsName.Content = name;
            newsText.Content = text;
            newsDateLabel.Content = date;
        }

        public void addNew(string name, string text)
        {
            newsName.Content = name;
            newsText.Content = text;
            newsDateLabel.Content = DateTime.Now;
        }
    }
}
