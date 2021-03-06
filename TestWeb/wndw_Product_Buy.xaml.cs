﻿using c_sharp_kursa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
    /// Interaction logic for wndw_Product_Buy.xaml
    /// </summary>
    public partial class wndw_Product_Buy : Window
    {
        private DatabaseConnection conn;
        private Payment payment;
        private Address address;
        private Shipment shipment;
        private string username;
        private List<Items> itemList;
        private int clickCounter;
        private string sum;
        private cartBox cB;

        public wndw_Product_Buy(DatabaseConnection conn, string username, List<Items> itemList, string sum, cartBox cB)
        {
            InitializeComponent();
            this.conn = conn;
            this.username = username;
            this.itemList = itemList;
            this.sum = sum;
            this.cB = cB;

            ctrlShipping.Init(conn);

            ctrlAddress.Visibility = Visibility.Hidden;
            ctrlPayment.Visibility = Visibility.Hidden;
            button1.Visibility = Visibility.Hidden;

            payment = null;
            address = null;
            shipment = null;

            clickCounter = 0;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (clickCounter == 0 && ctrlShipping.Execute() == true)         // shipping type
            {
                button1.Visibility = Visibility.Visible;

                shipment = ctrlShipping.GetShipment();
                ctrlShipping.Visibility = Visibility.Hidden;
                clickCounter = 1;

                ctrlAddress.Visibility = Visibility.Visible;
            }
            else if (clickCounter == 1 && ctrlAddress.Execute() == true)    // address 
            {
                address = ctrlAddress.GetAddress();
                ctrlAddress.Visibility = Visibility.Hidden;
                clickCounter = 2;

                ctrlPayment.Visibility = Visibility.Visible;
                button.Content = "Buy";
            }
            else if (clickCounter == 2 && ctrlPayment.Execute() == true)    // payment
            {
                payment = ctrlPayment.GetPayment();
                Buy();
            }
 
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (clickCounter == 1)      
            {
                button1.Visibility = Visibility.Hidden;
                clickCounter = 0;
                ctrlAddress.Visibility = Visibility.Hidden;
                ctrlShipping.Visibility = Visibility.Visible;
            }
            else if (clickCounter == 2)   
            {
                clickCounter = 1;
                ctrlPayment.Visibility = Visibility.Hidden;
                ctrlAddress.Visibility = Visibility.Visible;
                button.Content = ">>";
            }
        }

        private void Buy()
        {
            bool failed = false;
            foreach (Items item in itemList)
            {
                if (QuantityChecker(item.getIndex() + 1) < item.getCount())
                {
                    failed = true;
                    break;
                }
            }

            if (failed == false)
            {
                int usrID = GetUserID(username);
                int payID = PaymentRegister(payment.Date, payment.CardNumber, payment.CardHolderName, payment.CardHolderLastname, payment.ExpDate, sum);
                int addrID = AddressRegister(address.Name, address.Lastname, address.Phone, address.Email, address.Addresss, address.City);
                int ordID = OrderRegister(usrID, addrID, shipment.ID, payID);

                foreach (Items item in itemList)
                    BuyProduct(item.getIndex()+1, item.getCount(), GetUserID(username), addrID, shipment.ID, payID, ordID);

                MessageBox.Show("Successfully", "Buy product", MessageBoxButton.OK, MessageBoxImage.Information);

                cB.mwReadData();
                SendNotification();
                cB.RemoveAllOrders();
                this.Close();
            }
            else MessageBox.Show("Something went wrong", "Buy product", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private int PaymentRegister(string date, string cardNumber, string holdersName, string holdersLastname, string expDate, string money)
        {
            conn.WriteData("INSERT INTO Payments(Date, CardNumber, CardHoldersName, CardHoldersLastname, ExpDate, Amount)"
                            + " VALUES('" + date + "', '" + cardNumber + "', '" + holdersName + "', '" + holdersLastname + "', '" + expDate + "', '" + money.Replace(",", ".") + "')");

            return GetLastID("Payments");
        }

        private int AddressRegister(string name, string lastname, string phone, string email, string address, string city)
        {
            conn.WriteData("INSERT INTO Addresses(Name, Lastname, Phone, Email, Address, City)"
                           + "VALUES('" + name + "', '" + lastname + "', '" + phone + "', '"
                           + email + "', '" + address + "', '" + city + "')");

            return GetLastID("Addresses");
        }

        private int OrderRegister(int usrID, int addrID, int shipID, int payID)
        {
            conn.WriteData("INSERT INTO Orders (Users_ID, Addresses_ID, Shipping_methods_ID, Payments_ID, Date)"
                + " VALUES(" + usrID + ", " + addrID + ", " + shipID + ", " + payID + ", " + "NOW())");

            return GetLastID("Orders");
        }

        private int GetLastID(string table)
        {
            return Convert.ToInt32(conn.ReadData("select max(ID) from " + table)[0]);
        }

        private void BuyProduct(int productID, int quantity, int userID, int addrID, int shipID, int payID, int ordID)
        {
            conn.ReadData("select productBuy(" + productID + ", " + quantity + ", " + userID + ", " + addrID 
                + ", " + shipID + ", " + payID + ", " + ordID + ")");
        }

        private int GetUserID(string username)
        {
            return Convert.ToInt32(conn.ReadData("select ID from Users where Login='" + username + "'")[0]);
        }

        private int QuantityChecker(int id)
        {
            return Convert.ToInt32(conn.ReadData("SELECT Quantity FROM Products WHERE Products.ID = " + id)[0]);
        }

        public void SendNotification()
        {
            List<string> sellerList = cB.conn.ReadData("SELECT Email FROM Users WHERE Role='Seller'");
            MailMessage mail = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("aigs9256@gmail.com", "plzk92JA8a");

            mail.From = new MailAddress("aigs9256@gmail.com");

            string message = "Name: " + address.Name + "\n" +
                             "Lastname: " + address.Lastname + "\n" +
                             "Address: " + address.Addresss + " " + address.City + "\n" +
                             "Email: " + address.Email + "\n" +
                             "Shipment type: " + shipment.Name + "\n" +
                             "Products: ";
            for (int i = 0; i < itemList.Count; i++)
            {
                List<string> product = cB.conn.ReadData("SELECT Name FROM Products WHERE ID=" + itemList[i].getIndex());
                message += "\n" + product[0] + " x" + itemList[i].getCount();
            }


            try
            {
                for (int i = 0; i < sellerList.Count; i++)
                {
                    mail.To.Add(sellerList[i]);
                    mail.Subject = "New Order";
                    mail.Body = message;

                    smtp.Send(mail);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


    }
}
