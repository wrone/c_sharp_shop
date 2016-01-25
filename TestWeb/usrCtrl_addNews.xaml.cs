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
    /// Interaction logic for usrCtrl_addNews.xaml
    /// </summary>
    public partial class usrCtrl_addNews : UserControl
    {
        MainWindow mw;
        DatabaseConnection dbConn;
        public usrCtrl_addNews(MainWindow mw, DatabaseConnection dbConn)
        {
            this.mw = mw;
            this.dbConn = dbConn;
            InitializeComponent();
        }

        private void addEditButton_Click(object sender, RoutedEventArgs e)
        {
            int tmp = Convert.ToInt32(this.Tag);
            if (addEditButton.Content == "Add")
            {
                if (mw.newsClassList.Count >= 1)
                {
                    newsClass nC = new newsClass(mw.newsClassList[mw.newsClassList.Count - 1].getId() + 1, name.Text, text.Text);
                    mw.newsClassList.Add(nC);
                    dbConn.WriteData("INSERT INTO News (ID, Title, Text, Date) VALUES (" + (mw.newsClassList[mw.newsClassList.Count - 1].getId() + 1) + ", '" + name.Text + "', '" + text.Text + "', '" + DateTime.Now + "')");
                    MessageBox.Show("Succesfuly added.", "News additing", MessageBoxButton.OK, MessageBoxImage.Information);
                    mw.startPageButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                    mw.testButton2.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                }
                else if (mw.newsClassList.Count == 0)
                {
                    newsClass nC = new newsClass(1, name.Text, text.Text);
                    mw.newsClassList.Add(nC);
                    dbConn.WriteData("INSERT INTO News (ID, Title, Text, Date) VALUES (" + 1 + ", '" + name.Text + "', '" + text.Text + "', '" + DateTime.Now + "')");
                    MessageBox.Show("Succesfuly added.", "News additing", MessageBoxButton.OK, MessageBoxImage.Information);
                    mw.startPageButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                    mw.testButton2.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                }


            }
            else if(addEditButton.Content == "Edit")
            {
                mw.newsList[tmp].newsName.Content = name.Text;
                mw.newsList[tmp].newsName.Content = text.Text;

                mw.newsClassList[tmp].setName(name.Text);
                mw.newsClassList[tmp].setText(text.Text);

                dbConn.WriteData("UPDATE News SET Title = '"+ name.Text +"', Text = '"+ text.Text +"' WHERE ID = " + mw.newsClassList[tmp].getId());
                MessageBox.Show("Succesfuly edited.", "News editing", MessageBoxButton.OK, MessageBoxImage.Information);
                mw.startPageButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                mw.testButton2.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }
    }
}
