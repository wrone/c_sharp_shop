using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWeb
{
    public class Address
    {
        private int id;
        private string name;
        private string lastname;
        private string phone;
        private string email;
        private string address;
        private string city;

        public Address(int id, string name, string lastname, string phone, string email, string address, string city)
        {
            this.Name = name;
            this.Lastname = lastname;
            this.Phone = phone;
            this.Email = email;
            this.Addresss = address;
            this.City = city;
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string Lastname
        {
            get
            {
                return lastname;
            }

            set
            {
                lastname = value;
            }
        }

        public string Phone
        {
            get
            {
                return phone;
            }

            set
            {
                phone = value;
            }
        }

        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
            }
        }

        public string Addresss
        {
            get
            {
                return address;
            }

            set
            {
                address = value;
            }
        }

        public string City
        {
            get
            {
                return city;
            }

            set
            {
                city = value;
            }
        }



  
    }
}
