using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWeb
{
    public class Shipment
    {
        private int id;
        private string name;
        private decimal price;

        public Shipment(int id, string name, decimal price)
        {
            this.id = id;
            this.name = name;
            this.price = price;
        }

        public int ID
        {
            get { return id; }
            set { this.id = value;  }
        }
        public string Name
        {
            get { return name; }
            set { this.name = value; }
        }
        public decimal Price
        {
            get { return price; }
            set { this.price = value; }
        }

    }
}
