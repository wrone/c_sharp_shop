using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWeb
{
    public class Payment
    {
        private int id;
        private string date;
        private string cardNumber;
        private string cardHolderName;
        private string cardHolderLastname;
        private string expDate;
        private decimal amount;


        public Payment(int id, string date, string cardNumber, string cardHolderName, string cardHolderLastname, 
            string expDate, decimal amount )
        {
            this.Id = id;
            this.Date = date;
            this.CardNumber = cardNumber;
            this.CardHolderName = cardHolderName;
            this.CardHolderLastname = cardHolderLastname;
            this.ExpDate = expDate;
            this.Amount = amount;
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

        public string Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
            }
        }

        public string CardNumber
        {
            get
            {
                return cardNumber;
            }

            set
            {
                cardNumber = value;
            }
        }

        public string CardHolderName
        {
            get
            {
                return cardHolderName;
            }

            set
            {
                cardHolderName = value;
            }
        }

        public string CardHolderLastname
        {
            get
            {
                return cardHolderLastname;
            }

            set
            {
                cardHolderLastname = value;
            }
        }

        public string ExpDate
        {
            get
            {
                return expDate;
            }

            set
            {
                expDate = value;
            }
        }

        public decimal Amount
        {
            get
            {
                return amount;
            }

            set
            {
                amount = value;
            }
        }

 



    }
}
