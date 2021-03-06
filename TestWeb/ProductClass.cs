﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TestWeb
{
    public class ProductClass
    {
        // SELECT ID, Description, Release_date, End_date, Quantity, Price, Category, Manufacturer FROM Products
        private int ID, Quantity;
        private string Description, Release_date, End_date, Category, Manufacturer, Name;
        private double Price;
        private BitmapImage bmp;

        public ProductClass(int ID, string Name, string Description, string Release_date, string End_date, int Quantity, double Price, string Manufacturer, BitmapImage bmp, string Category)
        {
            this.ID = ID;
            this.Name = Name;
            this.Description = Description;
            this.Release_date = Release_date;
            this.End_date = End_date;
            this.Quantity = Quantity;
            this.Price = Price;
            this.Manufacturer = Manufacturer;
            this.bmp = bmp;
            this.Category = Category;
        }

        public string GetCategory()
        {
            return Category;
        }

        public int getId()
        {
            return ID;
        }

        public string getName()
        {
            return Name;
        }

        public string getDescription()
        {
            return Description;
        }

        public string getRelease_date()
        {
            return Release_date;
        }

        public string getEnd_date()
        {
            return End_date;
        }

        public int getQuantity()
        {
            return Quantity;
        }

        public double getPrice()
        {
            return Price;
        }

        public string getManufacturer()
        {
            return Manufacturer;
        }

        public BitmapImage getImage()
        {
            return bmp;
        }
    }
}
