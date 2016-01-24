using c_sharp_kursa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        MainWindow mw;
        DatabaseConnection dbConn;
        public newsHomePage(MainWindow mw, DatabaseConnection dbConn)
        {
            this.dbConn = dbConn;
            this.mw = mw;
            InitializeComponent();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Tag != null)
            {
                mw.newsClassList.RemoveAt(Convert.ToInt32(this.Tag));
                mw.newsList.RemoveAt(Convert.ToInt32(this.Tag));

                mw.startPageButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                mw.testButton2.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                dbConn.WriteData("DELETE FROM News WHERE ID = " + (mw.newsClassList[Convert.ToInt32(this.Tag)].getId() - 1));
            }
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Tag != null)
            {
                mw.homePage.Children.Clear();
                mw.hideOrUnhideAll(1);

                usrCtrl_addNews aN = new usrCtrl_addNews(mw, dbConn);
                aN.name.Text = mw.newsClassList[Convert.ToInt32(this.Tag)].getName();
                aN.text.Text = mw.newsClassList[Convert.ToInt32(this.Tag)].getText();
                aN.text.IsEnabled = true;
                aN.name.IsEnabled = true;
                aN.addEditButton.Visibility = Visibility.Visible;
                aN.addEditButton.Content = "Edit";
                aN.Tag = this.Tag;
                mw.homePage.Children.Add(aN);

            }

        }

        private void moreDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Tag != null)
            {
                mw.homePage.Children.Clear();
                mw.hideOrUnhideAll(1);

                usrCtrl_addNews aN = new usrCtrl_addNews(mw, dbConn);
                aN.name.Text = mw.newsClassList[Convert.ToInt32(this.Tag)].getName();
                aN.text.Text = mw.newsClassList[Convert.ToInt32(this.Tag)].getText();
                aN.text.IsEnabled = false;
                aN.name.IsEnabled = false;
                aN.addEditButton.Visibility = Visibility.Hidden;
                aN.Tag = this.Tag;
                mw.homePage.Children.Add(aN);
            }
        }
    }
}
