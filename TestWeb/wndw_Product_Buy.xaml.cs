﻿using c_sharp_kursa;
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
using System.Windows.Shapes;

namespace TestWeb
{
    /// <summary>
    /// Interaction logic for wndw_Product_Buy.xaml
    /// </summary>
    public partial class wndw_Product_Buy : Window
    {
        DatabaseConnection conn;
        Payment payment;
        Address address;
        Shipment shipment;

        string username;
        List<Items> itemList;
        int clickCounter;

        public wndw_Product_Buy(DatabaseConnection conn, string username, List<Items> itemList)
        {
            InitializeComponent();
            this.conn = conn;
            this.username = username;
            this.itemList = itemList;

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
                //canvas.Children.Remove(ctrlShipping);
                ctrlShipping.Visibility = Visibility.Hidden;
                clickCounter = 1;

                ctrlAddress.Visibility = Visibility.Visible;
            }
            else if (clickCounter == 1 && ctrlAddress.Execute() == true)    // address 
            {
                address = ctrlAddress.GetAddress();
                //canvas.Children.Remove(ctrlAddress);
                ctrlAddress.Visibility = Visibility.Hidden;
                clickCounter = 2;

                ctrlPayment.Visibility = Visibility.Visible;
                button.Content = "Buy";
            }
            else if (clickCounter == 2 && ctrlPayment.Execute() == true)    // payment
            {
                payment = ctrlPayment.GetPayment();
                //canvas.Children.Remove(ctrlPayment);
                Buy();
                MessageBox.Show("asdasdas");


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
            //int payID = PaymentRegister("2008-11-11", "asd", "asd", "asd", "2008-11-11", "14,87");
            //int addrID = AddressRegister("asd", "asd", "asd", "asd", "asd", "asd");
            //// BuyProduct(int productID, int quantity, int userID, int addrID, int shipID, int payID)
            //BuyProduct(1, 3, 1, 4, 1, 4);

            int payID = PaymentRegister(payment.Date, payment.CardNumber, payment.CardHolderName, payment.CardHolderLastname, payment.ExpDate, payment.Amount.ToString());
            int addrID = AddressRegister(address.Name, address.Lastname, address.Phone, address.Email, address.Addresss, address.City);

            foreach (Items item in itemList)
            {
                BuyProduct(item.getIndex()+1, item.getCount(), GetUserID(username), addrID, shipment.ID, payID);
            }

            //foreach (Items item in itemList)
            //    Console.WriteLine(item.getIndex() + " " + item.getCount());
            //BuyProduct(1, 3, GetUserID(username), addrID, shipment.ID, payID);
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

        private int GetLastID(string table)
        {
            return Convert.ToInt32(conn.ReadData("select max(ID) from " + table)[0]);
        }

        private void BuyProduct(int productID, int quantity, int userID, int addrID, int shipID, int payID)
        {
            conn.ReadData("select productBuy(" + productID + ", " + quantity + ", " + userID + ", " + addrID + ", " + shipID + ", " + payID + ")");
        }

        private int GetUserID(string username)
        {
            return Convert.ToInt32(conn.ReadData("select ID from Users where Login='" + username + "'")[0]);
        }


    }
}
