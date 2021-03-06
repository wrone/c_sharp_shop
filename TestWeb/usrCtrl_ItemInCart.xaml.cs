﻿using System;
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
    /// Interaction logic for usrCtrl_ItemInCart.xaml
    /// </summary>
    public partial class usrCtrl_ItemInCart : UserControl
    {
        cartBox cB;
        usrCtrl_CartInfo cartUsr;
        double price;
        public usrCtrl_ItemInCart(cartBox cB, usrCtrl_CartInfo cartUsr)
        {
            this.cB = cB;
            this.cartUsr = cartUsr;
            InitializeComponent();

            if (this.Tag != null)
            {
                price = cB.productList[cB.itemList[int.Parse(this.Tag.ToString())].getIndex()].getPrice() * Convert.ToInt32(countBox.Text);
                priceBox.Text = price.ToString();
            }
        }

        private void countBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (countBox.Text == "" || countBox.Text == "0")
                countBox.Text = "1";

            if (this.Tag != null && IsDigit(countBox.Text))
            {
                int tag = int.Parse(this.Tag.ToString());

                price = cB.productList[cB.itemList[tag].getIndex()].getPrice() * int.Parse(countBox.Text);
                priceBox.Text = price.ToString();
                cB.RecalculateEndPrice();

                cB.itemList[tag].setCount(int.Parse(countBox.Text));

                if (Convert.ToInt32(countBox.Text) > Convert.ToInt32(quantityLabel.Content))
                    countBox.Text = quantityLabel.Content.ToString();
            }
        }

        public bool IsDigit(string txt)
        {
            try
            {
                int.Parse(txt);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public double getPrice()
        {
            return price;
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Tag != null)
            {
                cB.itemListInCart.RemoveAt(Convert.ToInt32(this.Tag));
                cB.itemList.RemoveAt(Convert.ToInt32(this.Tag));

                cB.cartInfoButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                cB.cartInfoNumber.Content = cB.itemList.Count;
                cB.cartUsr.checkProductList();

            }
        }
    }
}
